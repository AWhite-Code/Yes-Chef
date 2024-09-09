using YesChefApp.ViewModels; // Ensure this is correct

namespace YesChefApp.Views // Ensure namespace matches
{
    public partial class RecipeListPage : ContentPage
    {
        public RecipeListPage()
        {
            InitializeComponent();
            BindingContext = new RecipeListViewModel(); // Ensure ViewModel is correctly referenced
        }
    }
}
