using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Yes_Chef.Data;
using Yes_Chef.Models;

namespace Yes_Chef.ViewModels
{
    public class RecipeListViewModel : BaseViewModel
    {
        private readonly YesChefContext _context;

        public ObservableCollection<Recipe> Recipes { get; }

        public RecipeListViewModel(YesChefContext context)
        {
            _context = context;
            Recipes = new ObservableCollection<Recipe>();

            // Load data
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            LoadDataCommand.Execute(null);

            AddRecipeCommand = new Command(async () => await NavigateToAddRecipePage());
        }
        public Command LoadDataCommand { get; }
        public Command AddRecipeCommand { get; }

        private async Task NavigateToAddRecipePage()
        {
            await Shell.Current.GoToAsync(nameof(AddRecipePage));
        }

        private async Task LoadDataAsync()
        {
            IsBusy = true;

            try
            {
                // Fetch recipes from the database
                var recipes = await _context.Recipes.ToListAsync();

                // Clear existing items
                Recipes.Clear();

                // Add fetched recipes to the collection
                foreach (var recipe in recipes)
                {
                    Recipes.Add(recipe);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
