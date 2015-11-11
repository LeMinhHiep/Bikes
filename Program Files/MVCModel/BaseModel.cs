using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCModel
{
    public abstract class BaseModel : IValidatableObject
    {
        public DateTime? EntryDate { get; set; }

        public int LocationID { get; set; }

        #region Implementation of IValidatableObject

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (false) yield return new ValidationResult("", new[] { "" });
        }

        #endregion
    }
}
