public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Initialized with default value
    public string Description { get; set; } = string.Empty; // Initialized with default value
    public string Ingredients { get; set; } = string.Empty; // Initialized with default value
    public string Instructions { get; set; } = string.Empty; // Initialized with default value
    public string ImageUrl { get; set; } = string.Empty; // Initialized with default value
}
