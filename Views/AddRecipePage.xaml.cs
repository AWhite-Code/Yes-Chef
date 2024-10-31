using Yes_Chef.ViewModels;

namespace Yes_Chef.Views
{
    public partial class AddRecipePage : ContentPage
    {
        public AddRecipePage(AddRecipeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
