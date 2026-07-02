using Fit_Elite.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Application.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public EnumRole LoginAs { get; set; }
    }
}
