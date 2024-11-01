using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yes_Chef.Models.Interfaces;

public class Tag : IAuditableEntity
{
    public int TagID { get; set; }
    public string TagName { get; set; }

    // Customization properties
    public string TextColor { get; set; }
    public string BackgroundColor { get; set; }
    public string BorderColor { get; set; }

    // Navigation property
    public ICollection<RecipeTag> RecipeTags { get; set; }

    // IAuditableEntity properties
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
