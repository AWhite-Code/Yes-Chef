using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

public class RecipeTagConfiguration : IEntityTypeConfiguration<RecipeTag>
{
    public void Configure(EntityTypeBuilder<RecipeTag> builder)
    {
        builder.ToTable("RecipeTags");  // Explicitly specify the existing table name

        // Configure the composite key
        builder.HasKey(rt => new { rt.RecipeID, rt.TagID });

        // Configure relationships
        builder.HasOne(rt => rt.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(rt => rt.RecipeID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rt => rt.Tag)
            .WithMany(t => t.RecipeTags)
            .HasForeignKey(rt => rt.TagID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}