using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitElite.Data.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [ForeignKey(nameof(Role))]
        public long RoleId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public Role Role { get; set; } = null!;

        // One User -> One Gym
        public Gym? Gym { get; set; }

        public ICollection<MemberSubscription> MemberSubscriptions { get; set; }
    = new List<MemberSubscription>();

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}