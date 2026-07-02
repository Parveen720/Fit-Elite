using Fit_Elite.Domain.common;
using Fit_Elite.Domain.Enums;

namespace Fit_Elite.Domain.Entities
{
    public class MemberSubscription : AuditableEntity
    {
        // Member
        public long UserId { get; set; }
        public User User { get; set; } = null!;

        // Selected Plan
        public long SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }

    }
}