using Fit_Elite.Domain.common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fit_Elite.Domain.Entities
{
    public class SubscriptionPlan : AuditableEntity
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

    }
}