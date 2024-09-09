using YesChefApp.ViewModels;

namespace YesChef.Views
{
    public partial class RecipeListPage : ContentPage
    {
        public RecipeListPage()
        {
            InitializeComponent();
            BindingContext = new RecipeListViewModel(); // Bind to ViewModel
        }
    }
}
