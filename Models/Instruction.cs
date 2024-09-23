using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yes_Chef.Models
{
    public class Instruction
    {
        [Key]
        public int InstructionID { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        [MaxLength(1000)]
        public string InstructionText { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? StepType { get; set; }

        // Navigation Property
        public Recipe Recipe { get; set; } = null!;
    }
}
