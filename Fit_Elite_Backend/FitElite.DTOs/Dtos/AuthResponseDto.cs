using System;
using System.Collections.Generic;
using System.Text;

namespace FitElite.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}