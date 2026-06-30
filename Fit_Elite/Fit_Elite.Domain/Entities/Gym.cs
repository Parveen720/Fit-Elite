using Fit_Elite.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Domain.Entities
{
    public class Gym : EntityBase
    {

        [Required]
        [StringLength(100)]
        public string GymName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string AddressLine1 { get; set; } = string.Empty;

        [StringLength(200)]
        public string? AddressLine2 { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(15)]
        public string ContactNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public TimeSpan OpeningTime { get; set; }

        public TimeSpan ClosingTime { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? ModifiedOn { get; set; }

        // Owner of the Gym
        public long GymOwnerId { get; set; }

        public User GymOwner { get; set; } = null!;
    }
}