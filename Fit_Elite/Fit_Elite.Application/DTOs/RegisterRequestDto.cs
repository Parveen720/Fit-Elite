using Fit_Elite.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Application.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        [EnumDataType(typeof(EnumRole), ErrorMessage = "Invalid Role Selection.")]
        public EnumRole Role { get; set; } = EnumRole.Member;
    }
}