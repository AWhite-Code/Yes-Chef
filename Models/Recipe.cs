using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBookApp.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Ingredients { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        // Initialize to prevent null references
        public ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
    }
}
