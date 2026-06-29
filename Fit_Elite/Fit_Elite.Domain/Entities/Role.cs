using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Domain.Entities
{
    public class Role : BaseEntity
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

    }
}