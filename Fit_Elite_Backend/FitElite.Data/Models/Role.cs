using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitElite.Data.Models
{
    public class Role
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; }
            = new List<User>();
    }
}

