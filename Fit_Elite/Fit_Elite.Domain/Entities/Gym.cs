using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Domain.Entities
{
    public class Gym : BaseEntity
    {

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

        public User GymOwner { get; set; } = null!;       
    }
}