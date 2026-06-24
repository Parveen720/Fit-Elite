using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitElite.Data.Models
{
    public class SubscriptionPlan
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Gym))]
        public long GymId { get; set; }

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int DurationInDays { get; set; }

        [StringLength(1000)]
        public string? Benefits { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public Gym Gym { get; set; } = null!;

        public ICollection<MemberSubscription> MemberSubscriptions { get; set; }
            = new List<MemberSubscription>();
    }
}