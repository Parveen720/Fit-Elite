using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Application.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!; 
    }
}