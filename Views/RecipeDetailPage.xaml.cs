using Yes_Chef.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Yes_Chef.Views
{
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();

            // Resolve the ViewModel from the DI container
            BindingContext = App.ServiceProvider.GetRequiredService<RecipeDetailViewModel>();
        }

        public RecipeDetailPage(RecipeDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
