using Fit_Elite.Domain.Common;

namespace Fit_Elite.Domain.Entities
{
    public class UserRole : EntityBase
    {
        public long UserId { get; set; }
        public User User { get; set; } = null!;

        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}