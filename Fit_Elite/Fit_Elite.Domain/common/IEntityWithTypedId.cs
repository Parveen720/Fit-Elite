using System;
using System.Collections.Generic;
using System.Text;

namespace Fit_Elite.Domain.Common
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}