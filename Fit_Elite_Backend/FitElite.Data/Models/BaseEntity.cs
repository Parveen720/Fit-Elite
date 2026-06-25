using System;

namespace FitElite.Data.Models
{
    public abstract class BaseEntity
    {

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

       
        public string? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

       
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}