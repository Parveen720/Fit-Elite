using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fit_Elite.Domain.Common
{
    public abstract class ValidatableObject : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            yield break;
        }
    }
}