using System.ComponentModel.DataAnnotations;
using Fit_Elite.Domain.Common;

namespace Fit_Elite.Domain.Entities
{
    public class Role : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

    }
}