using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

namespace Yes_Chef.Data.Configurations
{
    public class RecipeSectionConfiguration : IEntityTypeConfiguration<RecipeSection>
    {
        public void Configure(EntityTypeBuilder<RecipeSection> builder)
        {
            builder.HasKey(rs => rs.RecipeSectionID);

            builder.Property(rs => rs.SectionName)
                .IsRequired()
                .HasMaxLength(100);

            // Configure relationship with Recipe
            builder.HasOne(rs => rs.Recipe)
                .WithMany(r => r.Sections)
                .HasForeignKey(rs => rs.RecipeID)
                .OnDelete(DeleteBehavior.Restrict); 

            // Configure relationship with RecipeIngredients
            builder.HasMany(rs => rs.RecipeIngredients)
                .WithOne(ri => ri.RecipeSection)
                .HasForeignKey(ri => ri.RecipeSectionID)
                .OnDelete(DeleteBehavior.SetNull);

            // Add index for better performance when ordering sections
            builder.HasIndex(rs => new { rs.RecipeID, rs.DisplayOrder });
        }
    }
}