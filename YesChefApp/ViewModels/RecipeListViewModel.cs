using System.Collections.ObjectModel;
using System.ComponentModel;
using YesChefApp.Models;
using YesChefApp.Services;

namespace YesChefApp.ViewModels
{
    public class RecipeListViewModel : INotifyPropertyChanged
    {
        private readonly RecipeService _recipeService;
        public ObservableCollection<Recipe> Recipes { get; set; } // ObservableCollection of Recipes

        public RecipeListViewModel()
        {
            _recipeService = new RecipeService();
            Recipes = new ObservableCollection<Recipe>();
            LoadRecipes(); // Load recipes on initialization
        }

        private async void LoadRecipes()
        {
            Console.WriteLine("Loading recipes..."); // Debug statement
            var recipe = await _recipeService.GetTestRecipeAsync(); // Use GetTestRecipeAsync to fetch test recipe
            if (recipe != null)
            {
                Recipes.Add(recipe);
                OnPropertyChanged(nameof(Recipes));
                Console.WriteLine($"Loaded recipe: {recipe.Name}"); // Debug statement
            }
            else
            {
                Console.WriteLine("No recipes found."); // Debug statement
            }
        }

        private Recipe? _selectedRecipe; // Made nullable
        public Recipe? SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
                Console.WriteLine($"Selected recipe: {_selectedRecipe?.Name}"); // Debug statement
                if (_selectedRecipe != null)
                {
                    // Debug statement to confirm navigation
                    Console.WriteLine($"Navigating to RecipeDetailPage for recipe ID: {_selectedRecipe.Id}");
                    Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={_selectedRecipe.Id}");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Allow nullability
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
