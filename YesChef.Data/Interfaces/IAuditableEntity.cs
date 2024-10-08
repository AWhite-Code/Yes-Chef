using System;

namespace Yes_Chef.Models.Interfaces
{
    public interface IAuditableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
