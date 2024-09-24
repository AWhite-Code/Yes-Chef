using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yes_Chef.Data;
using Yes_Chef.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Yes_Chef.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly YesChefContext _context;

        // Properties bound to the UI
        private int _recipeID;
        public int RecipeID
        {
            get => _recipeID;
            set { _recipeID = value; OnPropertyChanged(); }
        }

        private string _recipeName;
        public string RecipeName
        {
            get => _recipeName;
            set { _recipeName = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        private TimeSpan? _prepTime;
        public TimeSpan? PrepTime
        {
            get => _prepTime;
            set { _prepTime = value; OnPropertyChanged(); }
        }

        private TimeSpan? _cookTime;
        public TimeSpan? CookTime
        {
            get => _cookTime;
            set { _cookTime = value; OnPropertyChanged(); }
        }

        public TimeSpan? TotalTime => PrepTime + CookTime;

        private string _tags;
        public string Tags
        {
            get => _tags;
            set { _tags = value; OnPropertyChanged(); }
        }

        private int _servingSize;
        public int ServingSize
        {
            get => _servingSize;
            set { _servingSize = value; OnPropertyChanged(); }
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; OnPropertyChanged(); }
        }

        public ObservableCollection<RecipeIngredient> Ingredients { get; }
        public ObservableCollection<Instruction> Instructions { get; }

        public RecipeDetailViewModel(YesChefContext context)
        {
            _context = context;
            Ingredients = new ObservableCollection<RecipeIngredient>();
            Instructions = new ObservableCollection<Instruction>();
        }

        // Implement IQueryAttributable
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("RecipeID"))
            {
                RecipeID = Convert.ToInt32(query["RecipeID"]);
                _ = LoadRecipeAsync();
            }
        }

        private async Task LoadRecipeAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Fetch the recipe including related data
                var recipe = await _context.Recipes
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.IngredientRef)
                    .Include(r => r.Instructions)
                    .FirstOrDefaultAsync(r => r.RecipeID == RecipeID);

                if (recipe != null)
                {
                    // Set properties
                    RecipeName = recipe.RecipeName;
                    Description = recipe.Description;
                    PrepTime = recipe.PrepTime;
                    CookTime = recipe.CookTime;
                    Tags = recipe.Tags;
                    ServingSize = recipe.ServingSize;
                    ImageSource = "placeholder_image.png"; // Update if actual images are available

                    // Load ingredients
                    Ingredients.Clear();
                    foreach (var ingredient in recipe.RecipeIngredients)
                    {
                        Ingredients.Add(ingredient);
                    }

                    // Load instructions
                    Instructions.Clear();
                    foreach (var instruction in recipe.Instructions.OrderBy(i => i.StepNumber))
                    {
                        Instructions.Add(instruction);
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions (e.g., display an error message)
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}