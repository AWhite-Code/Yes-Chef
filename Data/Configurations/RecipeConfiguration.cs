using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

namespace Yes_Chef.Data.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            // Unique Constraint on RecipeName
            builder.HasIndex(r => r.RecipeName)
                   .IsUnique();

            // Property Configurations
            builder.Property(r => r.RecipeName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(r => r.Description)
                   .HasMaxLength(1000);

            builder.Property(r => r.ServingSize)
                   .IsRequired();

            builder.Property(r => r.PrepTime)
                   .IsRequired(false);

            builder.Property(r => r.CookTime)
                   .IsRequired(false);

            // Seed Data
            builder.HasData(
                new Recipe
                {
                    RecipeID = 1,
                    RecipeName = "Spaghetti Bolognese",
                    Description = "A classic Italian pasta dish.",
                    DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DateModified = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ServingSize = 4,
                    PrepTime = new TimeSpan(0, 15, 0),
                    CookTime = new TimeSpan(0, 45, 0)
                },
                new Recipe
                {
                    RecipeID = 2,
                    RecipeName = "Chicken Curry",
                    Description = "A spicy and flavorful dish.",
                    DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DateModified = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ServingSize = 4,
                    PrepTime = new TimeSpan(0, 20, 0),
                    CookTime = new TimeSpan(1, 0, 0)
                }
            );
        }
    }
}
