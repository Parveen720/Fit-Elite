using Fit_Elite.Application.DTOs;

namespace Fit_Elite.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto);
    }
}