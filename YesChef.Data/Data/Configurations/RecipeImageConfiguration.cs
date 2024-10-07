using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

namespace Yes_Chef.Data.Configurations
{
    public class RecipeImageConfiguration : IEntityTypeConfiguration<RecipeImage>
    {
        public void Configure(EntityTypeBuilder<RecipeImage> builder)
        {
            // Relationship Configurations
            builder.HasOne(img => img.Recipe)
                   .WithMany(r => r.Images)
                   .HasForeignKey(img => img.RecipeID);

            // Property Configurations
            builder.Property(img => img.ImageUrl)
                   .IsRequired();

            // Seed Data (if applicable)
            // Add seed data here if needed
        }
    }
}
