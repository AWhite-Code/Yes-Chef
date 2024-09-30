using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yes_Chef.Models;

namespace Yes_Chef.Data.Configurations
{
    public class InstructionConfiguration : IEntityTypeConfiguration<Instruction>
    {
        public void Configure(EntityTypeBuilder<Instruction> builder)
        {
            // Property Configurations
            builder.Property(i => i.StepDescription)
                   .IsRequired();

            // Relationship Configurations
            builder.HasOne(i => i.Recipe)
                   .WithMany(r => r.Instructions)
                   .HasForeignKey(i => i.RecipeID);

            // Seed Data (if applicable)
            // Add seed data here if needed
        }
    }
}
