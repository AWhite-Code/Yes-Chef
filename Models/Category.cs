using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBookApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
    }
}
 