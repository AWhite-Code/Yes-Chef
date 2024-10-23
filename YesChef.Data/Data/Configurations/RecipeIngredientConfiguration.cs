using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

namespace Yes_Chef.Data.Configurations
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            // Primary Key
            builder.HasKey(ri => ri.RecipeIngredientID);

            // Enum Conversion
            builder.Property(ri => ri.Unit)
                   .HasConversion<string>()
                   .IsRequired();

            // Relationship Configurations
            builder.HasOne(ri => ri.Recipe)
                   .WithMany(r => r.RecipeIngredients)
                   .HasForeignKey(ri => ri.RecipeID);

            builder.HasOne(ri => ri.IngredientRef)
                   .WithMany(ir => ir.RecipeIngredients)
                   .HasForeignKey(ri => ri.IngredientRefID);

            // Property Configurations
            builder.Property(ri => ri.Quantity)
                .IsRequired()
                .HasPrecision(18, 4);

            // Seed Data (if applicable)
            // Add seed data here if needed
        }
    }
}
