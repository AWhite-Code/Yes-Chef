using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Yes_Chef.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand NavigateToRecipeListCommand { get; }

        public MainPageViewModel()
        {
            NavigateToRecipeListCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("RecipeListPage");
            });
        }
    }
}
