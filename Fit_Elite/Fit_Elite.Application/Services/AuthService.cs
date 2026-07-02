using Fit_Elite.Application.DTOs;
using Fit_Elite.Application.Exceptions;
using Fit_Elite.Application.Interfaces;
using Fit_Elite.Domain.Entities;
using Fit_Elite.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Fit_Elite.Application.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly iGenericRepository<User> _userRepository;
        private readonly iGenericRepository<Role> _roleRepository;
        private readonly IConfiguration _configuration;

        
        private static readonly EnumRole[] AllowedSelfRegisterRoles = {
            EnumRole.Member,
            EnumRole.GymOwner
        };

        public AuthService(
            iGenericRepository<User> userRepository,
            iGenericRepository<Role> roleRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerDto)
        {
            
            if (!AllowedSelfRegisterRoles.Contains(registerDto.Role))
            {
                throw new BadRequestException(
                    $"Invalid role '{registerDto.Role}'. Allowed roles: {string.Join(", ", AllowedSelfRegisterRoles)}."
                );
            }

         
            string cleanEmail = registerDto.Email.Trim().ToLower();
            if (await _userRepository.ExistsAsync(u => u.Email.ToLower() == cleanEmail))
            {
                throw new BadRequestException("This email is already registered!");
            }

            string targetRoleName = registerDto.Role.ToString();
            var role = await _roleRepository.FindSingleAsync(r => r.Name.ToLower() == targetRoleName.ToLower());
            if (role == null)
            {
                throw new BadRequestException($"Role '{targetRoleName}' does not exist in the database!");
            }

            string hashedPassword = BCryptNet.HashPassword(registerDto.Password);

            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = cleanEmail,
                PasswordHash = hashedPassword,
                PhoneNumber = registerDto.PhoneNumber
            };

            newUser.UserRole = new UserRole
            {
                User = newUser,
                RoleId = role.Id
            };

           
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveAsync();

            var token = GenerateJwtToken(newUser, role.Name);

            return new AuthResponseDto
            {
                Token = token,
                UserId = newUser.Id,
                Email = newUser.Email,
                FullName = newUser.FullName,
                Role = role.Name
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            
            string cleanEmail = loginDto.Email.Trim().ToLower();
            var user = await _userRepository.FindSingleAsync(
                u => u.Email.ToLower() == cleanEmail,
                u => u.UserRole,            
                u => u.UserRole.Role         
            );

            if (user == null)
            {
                throw new UnauthorizedException("Invalid Email or Password!");
            }

            bool isPasswordValid = BCryptNet.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedException("Invalid Email or Password!");
            }

            string roleName = user.UserRole?.Role?.Name ?? "Member";

            var token = GenerateJwtToken(user, roleName);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = roleName
            };
        }

        private string GenerateJwtToken(User user, string roleName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, roleName)
            };

            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Signing Key is missing in configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var durationMinutes = double.TryParse(_configuration["Jwt:DurationInMinutes"], out var parsedMinutes)
                ? parsedMinutes
                : 60;

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}