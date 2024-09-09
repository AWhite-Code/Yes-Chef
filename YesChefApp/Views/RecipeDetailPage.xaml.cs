using YesChefApp.Models;
using YesChefApp.ViewModels;
using YesChefApp.Services;

namespace YesChefApp.Views
{
    [QueryProperty(nameof(RecipeId), "recipeId")]
    public partial class RecipeDetailPage : ContentPage
    {
        private RecipeDetailViewModel? _viewModel; // Make _viewModel nullable

        public RecipeDetailPage()
        {
            InitializeComponent();
        }

        public int RecipeId { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine($"RecipeDetailPage appearing with RecipeId: {RecipeId}"); // Debug statement
            LoadRecipeDetails(); // Load recipe details when the page appears
        }

        private void LoadRecipeDetails()
        {
            // Fetch the recipe details using RecipeService
            var recipeService = new RecipeService();
            var recipe = recipeService.GetRecipeById(RecipeId); // Fetch recipe by ID

            if (recipe != null)
            {
                Console.WriteLine($"Recipe found: {recipe.Name}"); // Debug statement
                _viewModel = new RecipeDetailViewModel(recipe); // Update _viewModel with the fetched recipe
                BindingContext = _viewModel;
            }
            else
            {
                Console.WriteLine($"No recipe found with ID: {RecipeId}"); // Debug statement
            }
        }
    }
}
