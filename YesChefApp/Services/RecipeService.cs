using YesChefApp.Models; // Updated namespace
using System.Threading.Tasks;
using Microsoft.Data.Sqlite; 

namespace YesChefApp.Services
{
    public class RecipeService
    {
        private readonly string _connectionString = "Data Source=recipes.db"; 

        public async Task<Recipe> GetTestRecipeAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Recipes WHERE Id = 1"; 

            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new Recipe
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Ingredients = reader.GetString(3),
                    Instructions = reader.GetString(4),
                    ImageUrl = reader.GetString(5)
                };
            }
            return null;
        }
    }
}
