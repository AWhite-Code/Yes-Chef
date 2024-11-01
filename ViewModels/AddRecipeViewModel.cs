using System.Collections.ObjectModel;
using Yes_Chef.Models;
using Yes_Chef.Data;
using Yes_Chef.ViewModels;
using Yes_Chef.Views;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

public class AddRecipeViewModel : BaseViewModel
{
    private readonly IDbContextFactory<YesChefContext> _contextFactory;

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

    private async Task Cancel()
    {
        // Navigate back to the previous page
        await Shell.Current.GoToAsync("..");
    }

    public AddRecipeViewModel(IDbContextFactory<YesChefContext> contextFactory)
    {
        _contextFactory = contextFactory;
        Ingredients = new ObservableCollection<RecipeIngredient>();
        Instructions = new ObservableCollection<Instruction>();

        SaveCommand = new Command(async () => await SaveRecipe());
        CancelCommand = new Command(async () => await Cancel());
    }

    private async Task SaveRecipe()
    {
        using var context = _contextFactory.CreateDbContext();

        // Normalize the recipe name for comparison
        var normalizedRecipeName = this.RecipeName.Trim().ToLower();

        // Check if a recipe with the same name already exists
        var existingRecipe = await context.Recipes
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RecipeName.ToLower() == normalizedRecipeName);

        if (existingRecipe != null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "A recipe with this name already exists.", "OK");
            return;
        }

        var recipe = new Recipe
        {
            RecipeName = this.RecipeName.Trim(),
            Description = this.Description,
            ServingSize = this.ServingSize,
            PrepTime = this.PrepTime,
            CookTime = this.CookTime,
            // Other properties...
        };

        context.Recipes.Add(recipe);

        try
        {
            await context.SaveChangesAsync();

            // Navigate back to the recipe list
            await Shell.Current.GoToAsync("..");
        }
        catch (DbUpdateException dbEx)
        {
            // Capture detailed error information
            var errorMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            await Application.Current.MainPage.DisplayAlert("Database Error", errorMessage, "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }
}


