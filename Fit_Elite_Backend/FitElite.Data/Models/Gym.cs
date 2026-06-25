using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitElite.Data.Models
{
    public class Gym : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string GymName { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; } = string.Empty;

        public TimeSpan OpeningTime { get; set; }

        public TimeSpan ClosingTime { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsActive { get; set; } = true;

        

        public long GymOwnerId { get; set; }

        [ForeignKey(nameof(GymOwnerId))]
        public User GymOwner { get; set; } = null!;

        

        public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
            = new List<SubscriptionPlan>();

       
    }
}