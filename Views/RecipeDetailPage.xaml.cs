using Yes_Chef.ViewModels;
using Microsoft.Maui.Controls;

namespace Yes_Chef.Views
{
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage(RecipeDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
