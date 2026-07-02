using System;
using System.Collections.Generic;
using System.Text;

namespace Fit_Elite.Domain.common
{
    public abstract class EntityBaseWithTypedId<TId>
        : ValidatableObject, IEntityWithTypedId<TId>
    {
        public virtual TId Id { get; protected set; } = default!;
    }
}