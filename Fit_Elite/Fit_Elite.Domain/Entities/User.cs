using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fit_Elite.Domain.Entities
{
    public class User : BaseEntity
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
        public Role Role { get; set; } = null!;

        public bool IsActive { get; set; } = true;

       
        public ICollection<MemberSubscription> Subscriptions { get; set; } = new List<MemberSubscription>();
    }
}