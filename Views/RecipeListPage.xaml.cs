using Yes_Chef.Models;
using Yes_Chef.ViewModels;
using Microsoft.Maui.Controls;

namespace Yes_Chef.Views
{
    public partial class RecipeListPage : ContentPage
    {
        public RecipeListPage(RecipeListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Recipe;
                if (selectedRecipe != null)
                {
                    // Navigate to the detail page, passing the RecipeID as a query parameter
                    await Shell.Current.GoToAsync($"{nameof(RecipeDetailPage)}?RecipeID={selectedRecipe.RecipeID}");

                    // Deselect the item
                    ((CollectionView)sender).SelectedItem = null;
                }
            }
        }
    }
}
