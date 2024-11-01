using Yes_Chef.Models.Interfaces;
using Yes_Chef.Models;

public class RecipeTag : IAuditableEntity
{
    public int RecipeID { get; set; }
    public Recipe? Recipe { get; set; }

    public int TagID { get; set; }
    public Tag Tag { get; set; }

    // IAuditableEntity properties
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}

