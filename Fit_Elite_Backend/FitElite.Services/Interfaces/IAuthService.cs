using FitElite.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitElite.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
