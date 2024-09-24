using Yes_Chef.ViewModels;

namespace Yes_Chef.Views
{
    public partial class RecipeListPage : ContentPage
    {
        public RecipeListPage()
        {
            InitializeComponent();
            BindingContext = new RecipeListViewModel();
        }
    }
}
