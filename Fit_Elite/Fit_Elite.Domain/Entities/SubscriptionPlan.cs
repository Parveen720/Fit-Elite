using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fit_Elite.Domain.Entities
{
    public class SubscriptionPlan : BaseEntity
    {
        public long GymId { get; set; }

        public Gym Gym { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int DurationInDays { get; set; }

        [StringLength(1000)]
        public string? Benefits { get; set; }

        public bool IsActive { get; set; } = true;

    }
}