using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Yes_Chef.Data;
using Yes_Chef.Models;
using Yes_Chef.Views;

namespace Yes_Chef.ViewModels
{
    public class RecipeListViewModel : BaseViewModel
    {
        private readonly IDbContextFactory<YesChefContext> _contextFactory;

        public ObservableCollection<Recipe> Recipes { get; }

        public Command NavigateToDeletedRecipesCommand { get; }

        public RecipeListViewModel(IDbContextFactory<YesChefContext> contextFactory)
        {
            _contextFactory = contextFactory;
            Recipes = new ObservableCollection<Recipe>();

            LoadDataCommand = new Command(async () => await LoadDataAsync());
            AddRecipeCommand = new Command(async () => await NavigateToAddRecipePage());
            NavigateToDeletedRecipesCommand = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(DeletedRecipesPage)}"));
        }

        public Command LoadDataCommand { get; }
        public Command AddRecipeCommand { get; }

        private async Task NavigateToAddRecipePage()
        {
            await Shell.Current.GoToAsync(nameof(AddRecipePage));
        }

        public bool IsDataLoaded { get; private set; }
        private bool _isLoading;

        private async Task LoadDataAsync()
        {
            if (_isLoading)
                return;
            _isLoading = true;
            IsBusy = true;
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var recipes = await context.Recipes
                    .Where(r => r.DeletedAt == null)
                    .AsNoTracking()
                    .ToListAsync();

                Recipes.Clear();
                foreach (var recipe in recipes)
                {
                    Recipes.Add(recipe);
                }
            }
            finally
            {
                IsBusy = false;
                _isLoading = false;
            }
        }

    }
}
