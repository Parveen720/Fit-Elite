using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Domain.Enums
{
    public enum EnumRole
    {
        [Display(Name = "Admin")]
        Admin = 1,

        [Display(Name = "GymOwner")]
        GymOwner = 2,

        [Display(Name = "Member")]
        Member = 3
    }
}