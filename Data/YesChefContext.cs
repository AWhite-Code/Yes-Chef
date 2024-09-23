using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Windows.UI;
using Yes_Chef.Models;

namespace Yes_Chef.Data
{
    public class YesChefContext : DbContext
    {
        public YesChefContext(DbContextOptions<YesChefContext> options) : base(options)
        {
        }

        public DbSet<IngredientRef> IngredientRefs { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique Constraints
            modelBuilder.Entity<IngredientRef>()
                .HasIndex(i => i.IngredientName)
                .IsUnique();

            modelBuilder.Entity<Recipe>()
                .HasIndex(r => r.RecipeName)
                .IsUnique();

            // Relationships
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeID);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.IngredientRef)
                .WithMany(ir => ir.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientRefID);

            modelBuilder.Entity<Instruction>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Instructions)
                .HasForeignKey(i => i.RecipeID);

            modelBuilder.Entity<Image>()
                .HasOne(img => img.Recipe)
                .WithMany(r => r.Images)
                .HasForeignKey(img => img.RecipeID);
        }
    }
}
