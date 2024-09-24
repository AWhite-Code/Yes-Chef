using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yes_Chef.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }

        [Required]
        [MaxLength(200)]
        public string RecipeName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        public int ServingSize { get; set; }

        [MaxLength(200)]
        public string? Tags { get; set; }

        // Navigation Properties
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>(); 
        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
        public ICollection<RecipeImage> Images { get; set; } = new List<RecipeImage>();
    }
}
