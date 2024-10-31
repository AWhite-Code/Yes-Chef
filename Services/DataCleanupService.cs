using Microsoft.EntityFrameworkCore;
using Yes_Chef.Data;
using Yes_Chef.Helpers;
using Yes_Chef.Models;

namespace Yes_Chef.Services
{
    public class DataCleanupService
    {
        private readonly IDbContextFactory<YesChefContext> _contextFactory;

        public DataCleanupService(IDbContextFactory<YesChefContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CleanupDeletedRecipesAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var thresholdDate = DateTime.UtcNow - AppSettings.DeletionInterval;

            var recipesToDelete = await context.Recipes
                .Where(r => r.DeletedAt != null && r.DeletedAt <= thresholdDate)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Instructions)
                .ToListAsync();

            if (recipesToDelete.Any())
            {
                context.Recipes.RemoveRange(recipesToDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}
