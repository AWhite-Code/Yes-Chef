using System.ComponentModel;
using YesChefApp.Models;

namespace YesChefApp.ViewModels
{
    public class RecipeDetailViewModel : INotifyPropertyChanged
    {
        private Recipe _recipe;
        public Recipe Recipe
        {
            get => _recipe;
            set
            {
                _recipe = value;
                OnPropertyChanged(nameof(Recipe));
            }
        }

        public RecipeDetailViewModel(Recipe recipe)
        {
            _recipe = recipe ?? new Recipe(); // Initialize to a new Recipe if null
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
