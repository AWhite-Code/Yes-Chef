using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
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
        public DbSet<RecipeImage> RecipeImages { get; set; }

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

            modelBuilder.Entity<RecipeImage>()
                .HasOne(img => img.Recipe)
                .WithMany(r => r.Images)
                .HasForeignKey(img => img.RecipeID);

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeID = 1,
                    RecipeName = "Spaghetti Bolognese",
                    Description = "A classic Italian pasta dish.",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    ServingSize = 4,
                    Tags = "Italian, Pasta",
                    PrepTime = new TimeSpan(0, 15, 0),
                    CookTime = new TimeSpan(0, 45, 0)
                },
                new Recipe
                {
                    RecipeID = 2,
                    RecipeName = "Chicken Curry",
                    Description = "A spicy and flavorful dish.",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    ServingSize = 4,
                    Tags = "Indian, Spicy",
                    PrepTime = new TimeSpan(0, 20, 0),
                    CookTime = new TimeSpan(1, 0, 0)
                }
            );
        }
    }
}
