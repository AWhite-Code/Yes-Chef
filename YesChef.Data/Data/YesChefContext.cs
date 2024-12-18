using Microsoft.EntityFrameworkCore;
using Yes_Chef.Models;
using Yes_Chef.Models.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Yes_Chef.Data.Configurations;

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
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<RecipeSection> RecipeSections { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations from separate configuration classes
            modelBuilder.ApplyConfiguration(new IngredientRefConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeIngredientConfiguration());
            modelBuilder.ApplyConfiguration(new InstructionConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeImageConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeTagConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeSectionConfiguration());

            // Apply global query filters for all IAuditableEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
                }
            }

            // in future, seed Tag and RecipeTag data here
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

                // If the entity is Recipe, soft-delete related RecipeTags
                if (entry.Entity is Recipe recipe)
                {
                    // Load the related RecipeTags if not already loaded
                    if (recipe.RecipeTags == null || !recipe.RecipeTags.Any())
                    {
                        Entry(recipe).Collection(r => r.RecipeTags).Load();
                    }

                    foreach (var recipeTag in recipe.RecipeTags)
                    {
                        recipeTag.IsDeleted = true;
                        recipeTag.DeletedAt = DateTime.UtcNow;

                        // Mark the entity as modified to ensure it gets saved
                        Entry(recipeTag).State = EntityState.Modified;
                    }
                }
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
