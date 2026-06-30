using Fit_Elite.Domain.Common;
using Fit_Elite.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Domain.Entities
{
    public class MemberSubscription : EntityBase
    {
        // Member
        public long UserId { get; set; }
        public User User { get; set; } = null!;

       
        // Selected Plan
        public long SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public int RemainingDays { get; set; }

        public SubscriptionStatus Status { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? ModifiedOn { get; set; }
    }
}