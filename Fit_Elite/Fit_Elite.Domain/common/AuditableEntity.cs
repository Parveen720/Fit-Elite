using System;

namespace Fit_Elite.Domain.common
{
    public abstract class AuditableEntity : EntityBase
    {

        public bool IsDeleted { get; set; } = false;

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? ModifiedOn { get; set; }
    }
}