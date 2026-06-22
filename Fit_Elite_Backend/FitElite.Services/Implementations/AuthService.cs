using FitElite.Data.Models;
using FitElite.Data.Repositories;
using FitElite.DTOs;
using FitElite.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace FitElite.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IRepository<User> userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            dto.Email = dto.Email.Trim().ToLowerInvariant();

            var existingUser =
                await _userRepository.FirstOrDefaultAsync(
                x => x.Email == dto.Email);


            if (existingUser != null)
                return false;

            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            user.Password = hasher.HashPassword(user, dto.Password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {

            dto.Email = dto.Email.Trim().ToLowerInvariant();

            var user =
                await _userRepository.FirstOrDefaultAsync(
                    x => x.Email == dto.Email);

            if (user == null)
                throw new Exception("Invalid email or password");

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(
                user,
                user.Password,
                dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid email or password");

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Name),

                new Claim(
                    ClaimTypes.Email,
                    user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(
                    _configuration["Jwt:DurationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token),

                Expiration = expiration
            };
        }
    }
}