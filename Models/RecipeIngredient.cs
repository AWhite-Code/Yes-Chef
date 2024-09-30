using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yes_Chef.Models.Enums;
using Yes_Chef.Models.Interfaces;

namespace Yes_Chef.Models
{
    public class RecipeIngredient : IAuditableEntity
    {
        [Key]
        public int RecipeIngredientID { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }

        [ForeignKey("IngredientRef")]
        public int IngredientRefID { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public UnitType Unit { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Recipe Recipe { get; set; } = null!;
        public IngredientRef IngredientRef { get; set; } = null!;
    }
}
