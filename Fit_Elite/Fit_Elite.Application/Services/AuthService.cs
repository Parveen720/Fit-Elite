using Fit_Elite.Application.DTOs;
using Fit_Elite.Application.Interfaces;
using Fit_Elite.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Fit_Elite.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration; 

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerDto)
        {
            if (await _userRepository.EmailExistsAsync(registerDto.Email))
            {
                return new AuthResponseDto { IsSuccess = false, Message = "This email is already registered!" };
            }

            var role = await _userRepository.GetRoleByNameAsync(registerDto.Role);
            if (role == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = $"Role '{registerDto.Role}' does not exist!" };
            }

            string hashedPassword = BCryptNet.HashPassword(registerDto.Password);

            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                Role = role,
                IsActive = true
            };

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

           
            var token = GenerateJwtToken(newUser);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Registration successful!",
                Token = token,
                FullName = newUser.FullName,
                Role = role.Name
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid Email or Password!" };
            }

            bool isPasswordValid = BCryptNet.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid Email or Password!" };
            }

           
            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Login successful!",
                Token = token,
                FullName = user.FullName,
                Role = user.Role?.Name ?? "No Role"
            };
        }

       
        private string GenerateJwtToken(User user)
        {
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? "Member") 
            };

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

           
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}