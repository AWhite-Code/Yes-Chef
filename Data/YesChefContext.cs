using Microsoft.EntityFrameworkCore;
using Yes_Chef.Models;
using Yes_Chef.Models.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;

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
            // Apply configurations for all entities implementing IAuditableEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
                }
            }

            // Unique Constraints
            modelBuilder.Entity<IngredientRef>()
                .HasIndex(i => i.IngredientName)
                .IsUnique();

            modelBuilder.Entity<Recipe>()
                .HasIndex(r => r.RecipeName)
                .IsUnique();

            // Configure RecipeIngredient with single primary key and enum
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => ri.RecipeIngredientID);

            modelBuilder.Entity<RecipeIngredient>()
                .Property(ri => ri.Unit)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeID);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.IngredientRef)
                .WithMany(ir => ir.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientRefID);

            // Relationships for Instruction
            modelBuilder.Entity<Instruction>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Instructions)
                .HasForeignKey(i => i.RecipeID);

            // Relationships for RecipeImage
            modelBuilder.Entity<RecipeImage>()
                .HasOne(img => img.Recipe)
                .WithMany(r => r.Images)
                .HasForeignKey(img => img.RecipeID);

            // Data Validation and Constraints
            modelBuilder.Entity<Recipe>()
                .Property(r => r.RecipeName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<IngredientRef>()
                .Property(ir => ir.IngredientName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<RecipeIngredient>()
                .Property(ri => ri.Quantity)
                .IsRequired();

            modelBuilder.Entity<RecipeIngredient>()
                .Property(ri => ri.Unit)
                .IsRequired();

            // Seed Data Example
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeID = 1,
                    RecipeName = "Spaghetti Bolognese",
                    Description = "A classic Italian pasta dish.",
                    DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DateModified = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
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
                    DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DateModified = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ServingSize = 4,
                    Tags = "Indian, Spicy",
                    PrepTime = new TimeSpan(0, 20, 0),
                    CookTime = new TimeSpan(1, 0, 0)
                }
            );

            // Additional seed data for other entities can be added here
        }

        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var param = Expression.Parameter(type, "e");
            var prop = Expression.Property(param, nameof(IAuditableEntity.IsDeleted));
            var condition = Expression.Equal(prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, param);
            return lambda;
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            HandleSoftDeletes();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            HandleSoftDeletes();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSoftDeletes()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().Where(e => e.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;
            }
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.DateModified = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.UtcNow;
                }
            }
        }
    }
}
