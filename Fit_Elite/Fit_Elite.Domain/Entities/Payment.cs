using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fit_Elite.Domain.Enums;

namespace Fit_Elite.Domain.Entities
{
    public class Payment : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(MemberSubscription))]
        public long MemberSubscriptionId { get; set; }
        public MemberSubscription MemberSubscription { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TransactionId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}