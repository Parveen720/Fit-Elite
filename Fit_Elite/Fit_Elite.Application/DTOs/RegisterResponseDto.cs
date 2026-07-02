using System;
using System.Collections.Generic;
using System.Text;

namespace Fit_Elite.Application.DTOs
{
    public class RegisterResponseDto
    {
        public long UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Message { get; set; } = "Registration successful.";
    }

}
