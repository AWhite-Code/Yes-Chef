using YesChefApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace YesChefApp.Services
{
    public class RecipeService
    {
        private readonly string _connectionString = "Data Source=recipes.db"; // Path to SQLite database

        public RecipeService()
        {
            InitializeDatabase(); // Initialize the database on service creation
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            // Create the Recipes table if it doesn't exist
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Recipes (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Ingredients TEXT,
                    Instructions TEXT,
                    ImageUrl TEXT
                );";
            command.ExecuteNonQuery();

            // Optional: Add a default recipe if the table is empty (for testing purposes)
            command.CommandText = "SELECT COUNT(*) FROM Recipes";
            var result = command.ExecuteScalar(); // ExecuteScalar returns object?
            var count = (result != null && result != DBNull.Value) ? Convert.ToInt64(result) : 0; // Safe unboxing to long

            if (count == 0)
            {
                command.CommandText = @"
                    INSERT INTO Recipes (Name, Description, Ingredients, Instructions, ImageUrl)
                    VALUES ('Test Recipe', 'A delicious test recipe', 'Flour, Water', 'Mix ingredients and bake.', 'test.jpg');
                ";
                command.ExecuteNonQuery();
            }
        }

        // Method to get a test recipe asynchronously
        public async Task<Recipe?> GetTestRecipeAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Recipes LIMIT 1"; // Fetch the first recipe as a test

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

            return null; // Return null if no recipe is found
        }

        // Method to get a specific recipe by ID
        public Recipe? GetRecipeById(int recipeId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Recipes WHERE Id = $id";
            command.Parameters.AddWithValue("$id", recipeId);

            using var reader = command.ExecuteReader();
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
