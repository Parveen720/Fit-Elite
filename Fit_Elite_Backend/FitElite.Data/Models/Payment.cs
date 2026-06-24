using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitElite.Data.Enums;

namespace FitElite.Data.Models
{
    public class Payment
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(MemberSubscription))]
        public long MemberSubscriptionId { get; set; }

        [ForeignKey(nameof(Member))]
        public long MemberId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
            = PaymentStatus.Pending;

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TransactionId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties

        public MemberSubscription MemberSubscription { get; set; } = null!;

        public User Member { get; set; } = null!;
    }
}