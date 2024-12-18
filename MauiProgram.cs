using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yes_Chef.Data;
using Yes_Chef.ViewModels;
using Yes_Chef.Views;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Yes_Chef.Services;

namespace Yes_Chef
{
    public static class MauiProgram
    {

        private static void InitializeDatabaseAndCleanup(MauiApp app)
        {
            // Initialize the database
            using (var scope = app.Services.CreateScope())
            {
                var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<YesChefContext>>();
                using var context = contextFactory.CreateDbContext();
                context.Database.EnsureCreated();
            }

            // Run cleanup asynchronously
            Task.Run(async () =>
            {
                using var scope = app.Services.CreateScope();
                var cleanupService = scope.ServiceProvider.GetRequiredService<DataCleanupService>();
                try
                {
                    await cleanupService.CleanupDeletedRecipesAsync();
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    System.Diagnostics.Debug.WriteLine($"Error during cleanup: {ex.Message}");
                }
            });
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Build configuration using embedded resource
            var assembly = typeof(MauiProgram).Assembly;
            var stream = assembly.GetManifestResourceStream("Yes_Chef.appsettings.json");

            if (stream == null)
            {
                // Log this error appropriately for the platform
                System.Diagnostics.Debug.WriteLine("Could not find embedded appsettings.json");
                throw new InvalidOperationException(
                    "Could not find embedded appsettings.json. Ensure it exists and is marked as an Embedded Resource.");
            }

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);

            // Rest of your configuration...
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Services configuration...
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<RecipeListPage>();
            builder.Services.AddTransient<RecipeListViewModel>();
            builder.Services.AddTransient<RecipeDetailPage>();
            builder.Services.AddTransient<RecipeDetailViewModel>();
            builder.Services.AddTransient<AddRecipePage>();
            builder.Services.AddTransient<AddRecipeViewModel>();
            builder.Services.AddTransient<DeletedRecipesPage>();
            builder.Services.AddTransient<DeletedRecipesViewModel>();
            builder.Services.AddSingleton<DataCleanupService>();

            // DbContext configuration
            builder.Services.AddDbContextFactory<YesChefContext>(options =>
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
                }
                options.UseSqlServer(connectionString);
            });

            var app = builder.Build();
            InitializeDatabaseAndCleanup(app);
            return app;
        }
    }
}
