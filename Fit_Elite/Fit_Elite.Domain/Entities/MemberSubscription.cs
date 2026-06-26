using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fit_Elite.Domain.Enums;

namespace Fit_Elite.Domain.Entities
{
    public class MemberSubscription : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Member))]
        public long MemberId { get; set; }

        [InverseProperty("Subscriptions")]
        public User Member { get; set; } = null!;

        [ForeignKey(nameof(Gym))]
        public long GymId { get; set; }
        public Gym Gym { get; set; } = null!;


        [ForeignKey(nameof(SubscriptionPlan))]
        public long SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int RemainingDays { get; set; }

        public SubscriptionStatus Status { get; set; }

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}