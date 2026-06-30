using Fit_Elite.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fit_Elite.Domain.Entities
{
    public class SubscriptionPlan : EntityBase
    {
        public long GymId { get; set; }

        public Gym Gym { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int DurationInDays { get; set; }

        [StringLength(1000)]
        public string? Benefits { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? ModifiedOn { get; set; }
    }
}