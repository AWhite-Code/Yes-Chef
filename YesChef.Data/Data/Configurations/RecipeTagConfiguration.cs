using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Yes_Chef.Models;

public class RecipeTagConfiguration : IEntityTypeConfiguration<RecipeTag>
{
    public void Configure(EntityTypeBuilder<RecipeTag> builder)
    {
        builder.HasKey(rt => new { rt.RecipeID, rt.TagID });

        builder.HasOne(rt => rt.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(rt => rt.RecipeID)
            .OnDelete(DeleteBehavior.Restrict); // Prevent database-level cascade delete

        builder.HasOne(rt => rt.Tag)
            .WithMany(t => t.RecipeTags)
            .HasForeignKey(rt => rt.TagID)
            .OnDelete(DeleteBehavior.Restrict); // Prevent database-level cascade delete
    }
}
