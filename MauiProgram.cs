using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yes_Chef.Data;
using Yes_Chef.ViewModels;
using Yes_Chef.Views;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Yes_Chef
{
    public static class MauiProgram
    {
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


            // Configure DbContext
            builder.Services.AddDbContext<YesChefContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDbContextFactory<YesChefContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });


            // Build the app
            var app = builder.Build();

            // Initialize the database
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<YesChefContext>();
                db.Database.EnsureCreated();
            }

            return app;
        }
    }
}
