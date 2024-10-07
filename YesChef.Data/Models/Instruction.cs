using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yes_Chef.Models.Interfaces;

namespace Yes_Chef.Models
{
    public class Instruction : IAuditableEntity
    {
        [Key]
        public int InstructionID { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }

        [Required]
        public string StepDescription { get; set; } = string.Empty;

        public int StepNumber { get; set; } = 0;

        // Soft Delete Properties
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Audit Fields
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Recipe Recipe { get; set; } = null!;
    }
}
