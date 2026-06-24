using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitElite.Data.Enums;

namespace FitElite.Data.Models
{
    public class MemberSubscription
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Member))]
        public long MemberId { get; set; }

        [ForeignKey(nameof(Gym))]
        public long GymId { get; set; }

        [ForeignKey(nameof(SubscriptionPlan))]
        public long SubscriptionPlanId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }
            = SubscriptionStatus.Pending;

        // Navigation Properties

        public User Member { get; set; } = null!;

        public Gym Gym { get; set; } = null!;

        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}