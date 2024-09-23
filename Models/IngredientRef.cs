using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yes_Chef.Models
{
    public class IngredientRef
    {
        [Key]
        public int IngredientRefID { get; set; }

        [Required]
        [MaxLength(100)]
        public string IngredientName { get; set; }

        [MaxLength(100)]
        public string? IngredientCategory { get; set; }

        [Required]
        [MaxLength(100)]
        public string NormalizedIngredientName { get; set; }

        [MaxLength(50)]
        public string? UnitType { get; set; }

        // Navigation Property
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
