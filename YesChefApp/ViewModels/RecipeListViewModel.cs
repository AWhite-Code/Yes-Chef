using System.Collections.ObjectModel;
using System.ComponentModel;
using YesChefApp.Models;
using YesChefApp.Services;

namespace YesChefApp.ViewModels
{
    public class RecipeListViewModel : INotifyPropertyChanged
    {
        private readonly RecipeService _recipeService;
        public ObservableCollection<Recipe> Recipes { get; set; }

        public RecipeListViewModel()
        {
            _recipeService = new RecipeService();
            Recipes = new ObservableCollection<Recipe>();
            LoadRecipes();
        }

        private async void LoadRecipes()
        {
            var recipe = await _recipeService.GetTestRecipeAsync();
            if (recipe != null)
            {
                Recipes.Add(recipe);
                OnPropertyChanged(nameof(Recipes));
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
                if (_selectedRecipe != null)
                {
                    Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={_selectedRecipe.Id}");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Allow nullability
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
