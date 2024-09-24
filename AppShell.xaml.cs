using Yes_Chef.Views;
namespace Yes_Chef
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Routing
            Routing.RegisterRoute(nameof(RecipeListPage), typeof(RecipeListPage));
        }
    }
}
