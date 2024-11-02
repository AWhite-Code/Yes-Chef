using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Yes_Chef.Data;
using Yes_Chef.Models;

namespace Yes_Chef.ViewModels
{
    public class DeletedRecipesViewModel : BaseViewModel
    {
        private readonly IDbContextFactory<YesChefContext> _contextFactory;

        public ObservableCollection<Recipe> DeletedRecipes { get; }

        public DeletedRecipesViewModel(IDbContextFactory<YesChefContext> contextFactory)
        {
            _contextFactory = contextFactory;
            DeletedRecipes = new ObservableCollection<Recipe>();

            LoadDeletedRecipesCommand = new Command(async () => await LoadDeletedRecipesAsync());
            RestoreRecipeCommand = new Command<Recipe>(async (recipe) => await RestoreRecipeAsync(recipe));
            PermanentlyDeleteRecipeCommand = new Command<Recipe>(async (recipe) => await PermanentlyDeleteRecipeAsync(recipe));
        }

        public Command LoadDeletedRecipesCommand { get; }
        public Command<Recipe> RestoreRecipeCommand { get; }
        public Command<Recipe> PermanentlyDeleteRecipeCommand { get; }

        private async Task LoadDeletedRecipesAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var recipes = await context.Recipes
                    .IgnoreQueryFilters()
                    .Where(r => r.IsDeleted)
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .AsNoTracking()
                    .ToListAsync();

                DeletedRecipes.Clear();
                foreach (var recipe in recipes)
                {
                    DeletedRecipes.Add(recipe);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RestoreRecipeAsync(Recipe recipe)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                // Ignore query filters to find the soft-deleted recipe
                var recipeToRestore = await context.Recipes
                    .IgnoreQueryFilters()
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .FirstOrDefaultAsync(r => r.RecipeID == recipe.RecipeID);

                if (recipeToRestore != null)
                {
                    recipeToRestore.IsDeleted = false;
                    recipeToRestore.DeletedAt = null;

                    // Restore related RecipeTags if necessary
                    foreach (var recipeTag in recipeToRestore.RecipeTags)
                    {
                        recipeTag.IsDeleted = false;
                        recipeTag.DeletedAt = null;
                        context.Entry(recipeTag).State = EntityState.Modified;
                    }

                    await context.SaveChangesAsync();

                    DeletedRecipes.Remove(recipe);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to restore recipe: {ex.Message}", "OK");
            }
        }

        private async Task PermanentlyDeleteRecipeAsync(Recipe recipe)
        {
            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Permanently Delete",
                "Are you sure you want to permanently delete this recipe?",
                "Yes",
                "No");

            if (isConfirmed)
            {
                try
                {
                    using var context = _contextFactory.CreateDbContext();

                    // Ignore query filters to find the soft-deleted recipe
                    var recipeToDelete = await context.Recipes
                        .IgnoreQueryFilters()
                        .Include(r => r.RecipeIngredients)
                        .Include(r => r.Instructions)
                        .Include(r => r.RecipeTags)
                            .ThenInclude(rt => rt.Tag)
                        .FirstOrDefaultAsync(r => r.RecipeID == recipe.RecipeID);

                    if (recipeToDelete != null)
                    {
                        // Remove related entities
                        context.RecipeIngredients.RemoveRange(recipeToDelete.RecipeIngredients);
                        context.Instructions.RemoveRange(recipeToDelete.Instructions);
                        context.RecipeTags.RemoveRange(recipeToDelete.RecipeTags);

                        context.Recipes.Remove(recipeToDelete);
                        await context.SaveChangesAsync();

                        DeletedRecipes.Remove(recipe);
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete recipe: {ex.Message}", "OK");
                }
            }
        }
    }
}
