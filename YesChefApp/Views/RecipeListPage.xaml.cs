using YesChefApp.ViewModels;

namespace YesChefApp.Views
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
