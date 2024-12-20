﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yes_Chef.Data;

#nullable disable

namespace YesChef.Data.Migrations
{
    [DbContext(typeof(YesChefContext))]
    partial class YesChefContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<int>("TagID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("RecipeID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("RecipeTags", (string)null);
                });

            modelBuilder.Entity("Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagID"));

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("BorderColor")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TextColor")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("TagID");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("Yes_Chef.Models.IngredientRef", b =>
                {
                    b.Property<int>("IngredientRefID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientRefID"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IngredientCategory")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NormalizedIngredientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UnitType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IngredientRefID");

                    b.HasIndex("IngredientName")
                        .IsUnique();

                    b.ToTable("IngredientRefs");
                });

            modelBuilder.Entity("Yes_Chef.Models.Instruction", b =>
                {
                    b.Property<int>("InstructionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructionID"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<string>("StepDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.HasKey("InstructionID");

                    b.HasIndex("RecipeID");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("Yes_Chef.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeID"));

                    b.Property<TimeSpan?>("CookTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OriginalServingSize")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("PrepTime")
                        .HasColumnType("time");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ServingSize")
                        .HasColumnType("int");

                    b.HasKey("RecipeID");

                    b.HasIndex("RecipeName")
                        .IsUnique()
                        .HasFilter("[IsDeleted] = 0");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            RecipeID = 1,
                            CookTime = new TimeSpan(0, 0, 45, 0, 0),
                            DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            DateModified = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "A classic Italian pasta dish.",
                            IsDeleted = false,
                            OriginalServingSize = 0,
                            PrepTime = new TimeSpan(0, 0, 15, 0, 0),
                            RecipeName = "Spaghetti Bolognese",
                            ServingSize = 4
                        },
                        new
                        {
                            RecipeID = 2,
                            CookTime = new TimeSpan(0, 1, 0, 0, 0),
                            DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            DateModified = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "A spicy and flavorful dish.",
                            IsDeleted = false,
                            OriginalServingSize = 0,
                            PrepTime = new TimeSpan(0, 0, 20, 0, 0),
                            RecipeName = "Chicken Curry",
                            ServingSize = 4
                        });
                });

            modelBuilder.Entity("Yes_Chef.Models.RecipeImage", b =>
                {
                    b.Property<int>("RecipeImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeImageID"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.HasKey("RecipeImageID");

                    b.HasIndex("RecipeID");

                    b.ToTable("RecipeImages");
                });

            modelBuilder.Entity("Yes_Chef.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeIngredientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeIngredientID"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisplayOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("IngredientRefID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("OriginalQuantity")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Quantity")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<int?>("RecipeSectionID")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecipeIngredientID");

                    b.HasIndex("IngredientRefID");

                    b.HasIndex("RecipeSectionID");

                    b.HasIndex("RecipeID", "RecipeSectionID", "DisplayOrder");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("Yes_Chef.Models.RecipeSection", b =>
                {
                    b.Property<int>("RecipeSectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeSectionID"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RecipeSectionID");

                    b.HasIndex("RecipeID", "DisplayOrder");

                    b.ToTable("RecipeSections");
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.HasOne("Yes_Chef.Models.Recipe", "Recipe")
                        .WithMany("RecipeTags")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Tag", "Tag")
                        .WithMany("RecipeTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Yes_Chef.Models.Instruction", b =>
                {
                    b.HasOne("Yes_Chef.Models.Recipe", "Recipe")
                        .WithMany("Instructions")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Yes_Chef.Models.RecipeImage", b =>
                {
                    b.HasOne("Yes_Chef.Models.Recipe", "Recipe")
                        .WithMany("Images")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Yes_Chef.Models.RecipeIngredient", b =>
                {
                    b.HasOne("Yes_Chef.Models.IngredientRef", "IngredientRef")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientRefID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yes_Chef.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yes_Chef.Models.RecipeSection", "RecipeSection")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeSectionID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("IngredientRef");

                    b.Navigation("Recipe");

                    b.Navigation("RecipeSection");
                });

            modelBuilder.Entity("Yes_Chef.Models.RecipeSection", b =>
                {
                    b.HasOne("Yes_Chef.Models.Recipe", "Recipe")
                        .WithMany("Sections")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Tag", b =>
                {
                    b.Navigation("RecipeTags");
                });

            modelBuilder.Entity("Yes_Chef.Models.IngredientRef", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("Yes_Chef.Models.Recipe", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Instructions");

                    b.Navigation("RecipeIngredients");

                    b.Navigation("RecipeTags");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Yes_Chef.Models.RecipeSection", b =>
                {
                    b.Navigation("RecipeIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
