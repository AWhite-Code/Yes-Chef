using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yes_Chef.Models
{
    public class RecipeIngredient
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
        [MaxLength(50)]
        public string Unit { get; set; } = string.Empty;

        // Navigation Properties
        public Recipe Recipe { get; set; } = null!; 
        public IngredientRef IngredientRef { get; set; } = null!; 
    }
}
