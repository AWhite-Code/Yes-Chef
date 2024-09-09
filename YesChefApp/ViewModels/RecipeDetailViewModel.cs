using System.ComponentModel;
using YesChefApp.Models; // Updated namespace

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
            Recipe = recipe;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
