﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yes_Chef.Models.Interfaces;

namespace Yes_Chef.Models
{
    public class Recipe : IAuditableEntity
    {
        [Key]
        public int RecipeID { get; set; }

        [Required]
        [MaxLength(200)]
        public string RecipeName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        [Required]
        public int ServingSize { get; set; }

        [Required]
        public int OriginalServingSize { get; set; }  // Added for scaling calculations

        public TimeSpan? PrepTime { get; set; }
        public TimeSpan? CookTime { get; set; }

        [NotMapped]
        public TimeSpan? TotalTime
        {
            get
            {
                return (PrepTime ?? TimeSpan.Zero) + (CookTime ?? TimeSpan.Zero);
            }
        }

        // Soft Delete Properties
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation Properties
        public ICollection<RecipeSection> Sections { get; set; } = new List<RecipeSection>();
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
        public ICollection<RecipeImage> Images { get; set; } = new List<RecipeImage>();
        public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();

        [NotMapped]
        public string TagsDisplay
        {
            get
            {
                if (RecipeTags != null && RecipeTags.Any())
                {
                    return string.Join(", ", RecipeTags.Select(rt => rt.Tag.TagName));
                }
                return string.Empty;
            }
        }
    }
}