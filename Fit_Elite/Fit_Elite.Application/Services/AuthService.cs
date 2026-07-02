using Fit_Elite.Application.DTOs;
using Fit_Elite.Application.Exceptions;
using Fit_Elite.Application.Interfaces;
using Fit_Elite.Domain.Entities;
using Fit_Elite.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            iGenericRepository<User> userRepository,
            iGenericRepository<Role> roleRepository,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerDto)
        {
            _logger.LogInformation("Registration started for {Email}", registerDto.Email);

            string cleanEmail = registerDto.Email.Trim().ToLower();

            if (await _userRepository.ExistsAsync(u => u.Email.ToLower() == cleanEmail))
            {
                _logger.LogWarning("Registration failed. Email already exists: {Email}", cleanEmail);
                throw new BadRequestException("This email is already registered!");
            }

            string targetRoleName = nameof(EnumRole.Member);

            var role = await _roleRepository.FindSingleAsync(r => r.Name == targetRoleName);

            if (role == null)
            {
                _logger.LogError("Registration failed. Role {Role} does not exist.", targetRoleName);
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

            _logger.LogInformation(
                "User registered successfully. UserId: {UserId}, Email: {Email}",
                newUser.Id,
                newUser.Email);

            return new RegisterResponseDto
            {
                UserId = newUser.Id,
                Email = newUser.Email,
                FullName = newUser.FullName,
                Role = role.Name,
                Message = "Registration successful."
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            _logger.LogInformation("Login attempt for {Email}", loginDto.Email);

            string cleanEmail = loginDto.Email.Trim().ToLower();

            var user = await _userRepository.FindSingleAsync(
                u => u.Email.ToLower() == cleanEmail,
                u => u.UserRole,
                u => u.UserRole.Role);

            if (user == null)
            {
                _logger.LogWarning("Login failed. User not found: {Email}", cleanEmail);
                throw new UnauthorizedException("Invalid Email or Password!");
            }

            if (!BCryptNet.Verify(loginDto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Login failed. Invalid password for {Email}", cleanEmail);
                throw new UnauthorizedException("Invalid Email or Password!");
            }

            if (user.UserRole?.Role == null)
            {
                _logger.LogError("Login failed. Role not assigned for user {Email}", cleanEmail);
                throw new UnauthorizedException("User role not assigned.");
            }

            string roleName = user.UserRole.Role.Name;

            if (!roleName.Equals(loginDto.LoginAs.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning(
                    "Login failed. User {Email} selected role {SelectedRole}, actual role {ActualRole}",
                    cleanEmail,
                    loginDto.LoginAs,
                    roleName);

                throw new UnauthorizedException($"Please login as {roleName}.");
            }

            string token = GenerateJwtToken(user, roleName);

            _logger.LogInformation(
                "Login successful. UserId: {UserId}, Email: {Email}, Role: {Role}",
                user.Id,
                user.Email,
                roleName);

            return new LoginResponseDto
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

            var durationMinutes = double.TryParse(
                _configuration["Jwt:DurationInMinutes"],
                out var parsedMinutes)
                ? parsedMinutes
                : 60;

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}