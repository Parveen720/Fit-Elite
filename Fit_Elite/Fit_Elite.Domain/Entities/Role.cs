using Fit_Elite.Domain.common;
using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Domain.Entities
{
    public class Role : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

       public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}