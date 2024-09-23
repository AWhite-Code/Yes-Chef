﻿using System;
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
        public string RecipeName { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        public int ServingSize { get; set; }

        [MaxLength(200)]
        public string? Tags { get; set; }

        // Navigation Properties
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public ICollection<Instruction> Instructions { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
