using System.Collections.ObjectModel;
using Yes_Chef.Models;
using Yes_Chef.Data;
using Yes_Chef.ViewModels;

public class AddRecipeViewModel : BaseViewModel
{
    private readonly YesChefContext _context;

    // Properties bound to the UI
    public string RecipeName { get; set; }
    public string Description { get; set; }
    public int ServingSize { get; set; }
    public TimeSpan? PrepTime { get; set; }
    public TimeSpan? CookTime { get; set; }

    public ObservableCollection<RecipeIngredient> Ingredients { get; }
    public ObservableCollection<Instruction> Instructions { get; }

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }

    public AddRecipeViewModel(YesChefContext context)
    {
        _context = context;
        Ingredients = new ObservableCollection<RecipeIngredient>();
        Instructions = new ObservableCollection<Instruction>();

        SaveCommand = new Command(async () => await SaveRecipe());
        CancelCommand = new Command(async () => await Cancel());
    }

    private async Task SaveRecipe()
    {
        var recipe = new Recipe
        {
            RecipeName = this.RecipeName,
            Description = this.Description,
            ServingSize = this.ServingSize,
            PrepTime = this.PrepTime,
            CookTime = this.CookTime,
            // Initialize other properties here
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        // Navigate back to the recipe list
        await Shell.Current.GoToAsync("..");
    }

    private async Task Cancel()
    {
        // Navigate back without saving
        await Shell.Current.GoToAsync("..");
    }
}