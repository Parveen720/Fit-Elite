using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitElite.Data.Enums;

namespace FitElite.Data.Models
{
    public class MemberSubscription : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Member))]
        public long MemberId { get; set; }
        public User Member { get; set; } = null!;


        [ForeignKey(nameof(SubscriptionPlan))]
        public long SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}