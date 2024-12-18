using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {

        builder.ToTable("Tag");  // Explicitly specify the table name

        builder.HasKey(t => t.TagID);

        builder.Property(t => t.TagName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.TextColor)
            .HasMaxLength(7); // For hex color codes (e.g., "#FFFFFF")

        builder.Property(t => t.BackgroundColor)
            .HasMaxLength(7);

        builder.Property(t => t.BorderColor)
            .HasMaxLength(7);

        // Configure relationships
        builder.HasMany(t => t.RecipeTags)
            .WithOne(rt => rt.Tag)
            .HasForeignKey(rt => rt.TagID);
    }
}
