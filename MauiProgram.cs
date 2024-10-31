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

            // Build configuration
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Yes_Chef.appsettings.json");
            if (stream == null)
            {
                throw new InvalidOperationException("Could not find embedded resource 'Yes_Chef.appsettings.json'");
            }

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);

            // Configure fonts and app
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Configure services
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



            // Configure DbContext
            builder.Services.AddDbContextFactory<YesChefContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });


            // Build the app
            var app = builder.Build();

            InitializeDatabaseAndCleanup(app);

            return app;
        }
    }
}
