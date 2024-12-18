using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

namespace Yes_Chef.Data.Configurations
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasKey(ri => ri.RecipeIngredientID);

            builder.Property(ri => ri.Quantity)
                .HasPrecision(18, 4);

            builder.Property(ri => ri.OriginalQuantity)
                .HasPrecision(18, 4);

            builder.Property(ri => ri.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(ri => ri.RecipeSectionID)
                .IsRequired(false);

            // Configure relationship with Recipe
            builder.HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Index for efficient querying
            builder.HasIndex(ri => new { ri.RecipeID, ri.RecipeSectionID, ri.DisplayOrder });
        }
    }
}