using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

namespace Yes_Chef.Data.Configurations
{
    public class IngredientRefConfiguration : IEntityTypeConfiguration<IngredientRef>
    {
        public void Configure(EntityTypeBuilder<IngredientRef> builder)
        {
            // Unique Constraint on IngredientName
            builder.HasIndex(ir => ir.IngredientName)
                   .IsUnique();

            // Property Configurations
            builder.Property(ir => ir.IngredientName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(ir => ir.NormalizedIngredientName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(ir => ir.IngredientCategory)
                   .HasMaxLength(100);

            builder.Property(ir => ir.UnitType)
                   .HasMaxLength(50);

            // Seed Data (if applicable)
            // Add seed data here if needed
        }
    }
}
