using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yes_Chef.Data;
using Yes_Chef.Models;
using Yes_Chef.ViewModels;
using Yes_Chef.Views;

public class AddRecipeViewModel : BaseViewModel
{
    private readonly IDbContextFactory<YesChefContext> _contextFactory;

    // Properties bound to the UI
    public string RecipeName { get; set; }
    public string Description { get; set; }

    // ServingSize
    private int _servingSize;
    public int ServingSize
    {
        get => _servingSize;
        set => SetProperty(ref _servingSize, value);
    }

    private string _servingSizeText;
    public string ServingSizeText
    {
        get => _servingSizeText;
        set
        {
            if (SetProperty(ref _servingSizeText, value))
            {
                if (int.TryParse(value, out int result))
                {
                    ServingSize = result;
                }
                else
                {
                    // Handle invalid input
                    ServingSize = 0;
                }
            }
        }
    }

    // PrepTime
    private TimeSpan? _prepTime;
    public TimeSpan? PrepTime
    {
        get => _prepTime;
        set => SetProperty(ref _prepTime, value);
    }

    private string _prepTimeText;
    public string PrepTimeText
    {
        get => _prepTimeText;
        set
        {
            if (SetProperty(ref _prepTimeText, value))
            {
                if (double.TryParse(value, out double minutes))
                {
                    PrepTime = TimeSpan.FromMinutes(minutes);
                }
                else
                {
                    PrepTime = null;
                }
            }
        }
    }

    // CookTime
    private TimeSpan? _cookTime;
    public TimeSpan? CookTime
    {
        get => _cookTime;
        set => SetProperty(ref _cookTime, value);
    }

    private string _cookTimeText;
    public string CookTimeText
    {
        get => _cookTimeText;
        set
        {
            if (SetProperty(ref _cookTimeText, value))
            {
                if (double.TryParse(value, out double minutes))
                {
                    CookTime = TimeSpan.FromMinutes(minutes);
                }
                else
                {
                    CookTime = null;
                }
            }
        }
    }

    public ObservableCollection<RecipeIngredient> Ingredients { get; }
    public ObservableCollection<Instruction> Instructions { get; }

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }

    public AddRecipeViewModel(IDbContextFactory<YesChefContext> contextFactory)
    {
        _contextFactory = contextFactory;
        Ingredients = new ObservableCollection<RecipeIngredient>();
        Instructions = new ObservableCollection<Instruction>();

        SaveCommand = new Command(async () => await SaveRecipe());
        CancelCommand = new Command(async () => await Cancel());

        // Initialize text properties
        ServingSizeText = string.Empty;
        PrepTimeText = string.Empty;
        CookTimeText = string.Empty;
    }

    private async Task Cancel()
    {
        // Navigate back to the previous page
        await Shell.Current.GoToAsync("..");
    }

    private async Task SaveRecipe()
    {
        using var context = _contextFactory.CreateDbContext();

        var recipeName = this.RecipeName?.Trim();

        if (string.IsNullOrEmpty(recipeName))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Recipe name is required.", "OK");
            return;
        }

        // Check if a recipe with the same name already exists (including soft-deleted ones)
        var existingRecipe = await context.Recipes
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RecipeName == recipeName);

        if (existingRecipe != null)
        {
            if (existingRecipe.IsDeleted)
            {
                // Option 1: Offer to restore the soft-deleted recipe
                var restore = await Application.Current.MainPage.DisplayAlert(
                    "Recipe Exists",
                    "A recipe with this name was previously deleted. Do you want to restore it?",
                    "Yes",
                    "No");

                if (restore)
                {
                    // Restore the soft-deleted recipe
                    existingRecipe.IsDeleted = false;
                    existingRecipe.DeletedAt = null;
                    context.Recipes.Update(existingRecipe);
                    await context.SaveChangesAsync();

                    // Navigate back to the recipe list
                    await Shell.Current.GoToAsync("..");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please choose a different recipe name.", "OK");
                    return;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "A recipe with this name already exists.", "OK");
                return;
            }
        }

        var recipe = new Recipe
        {
            RecipeName = recipeName,
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
