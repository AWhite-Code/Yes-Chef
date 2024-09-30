﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yes_Chef.Models
{
    public class RecipeImage
    {
        [Key]
        public int ImageID { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageURL { get; set; } = string.Empty;

        public DateTime DateUploaded { get; set; }

        // Navigation Property
        public Recipe Recipe { get; set; } = null!;

        // Soft Delete Properties
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
