using Fit_Elite.Domain.Enums;

namespace Fit_Elite.Domain.Entities
{
    public class MemberSubscription : BaseEntity
    {
        public long MemberId { get; set; }

        public User Member { get; set; } = null!;

        public long GymId { get; set; }
        public Gym Gym { get; set; } = null!;
        public long SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int RemainingDays { get; set; }

        public SubscriptionStatus Status { get; set; }

    }
}