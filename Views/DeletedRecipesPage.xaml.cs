using Yes_Chef.ViewModels;
using Yes_Chef.Models;

namespace Yes_Chef.Views
{
    public partial class DeletedRecipesPage : ContentPage
    {
        private readonly DeletedRecipesViewModel _viewModel;

        public DeletedRecipesPage(DeletedRecipesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadDeletedRecipesCommand.Execute(null);
        }
    }
}
