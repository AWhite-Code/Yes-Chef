using System.Collections.ObjectModel;
using System.ComponentModel;
using YesChefApp.Models; // Updated namespace
using YesChefApp.Services; // Updated namespace

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

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
