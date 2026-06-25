using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitElite.Data.Models
{
    public class Role : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navigation Property: One Role can belong to Many Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}