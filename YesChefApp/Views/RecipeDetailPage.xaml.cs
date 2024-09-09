using YesChefApp.Models; // Correct using directive
using YesChefApp.ViewModels;

namespace YesChefApp.Views
{
    [QueryProperty(nameof(Recipe), "recipeId")]
    public partial class RecipeDetailPage : ContentPage
    {
        private RecipeDetailViewModel _viewModel; // Non-nullable field, initialized in constructor

        public RecipeDetailPage()
        {
            InitializeComponent();
            _viewModel = new RecipeDetailViewModel(new Recipe()); // Initialize with a default Recipe
            BindingContext = _viewModel; // Set the BindingContext
        }

        public int RecipeId { get; set; }

        public Recipe Recipe
        {
            set
            {
                _viewModel = new RecipeDetailViewModel(value); // Update _viewModel when Recipe is set
                BindingContext = _viewModel;
            }
        }
    }
}
