using Fit_Elite.Domain.common;
using Fit_Elite.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fit_Elite.Domain.Entities
{
    public class Payment : AuditableEntity
    {
       
        public long UserId { get; set; }
        public User User { get; set; } = null!;

        public long SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TransactionId { get; set; }

        [StringLength(100)]
        public string? PaymentGateway { get; set; }

        public DateTimeOffset PaymentDate { get; set; } = DateTimeOffset.UtcNow;

    }
}