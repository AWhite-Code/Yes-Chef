using Microsoft.Extensions.DependencyInjection;
using Yes_Chef.Views;
using Yes_Chef.ViewModels;
using Yes_Chef.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Yes_Chef
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Configure fonts and app
            builder
                .UseMauiApp<App>() // We'll modify this line below
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

            // Configure DbContext with SQLite
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "YesChefDatabase.db3");

            builder.Services.AddDbContext<YesChefContext>(options =>
            {
                options.UseSqlite($"Filename={dbPath}");
            });

            // Build the app
            var app = builder.Build();

            // Initialize the database
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<YesChefContext>();
                db.Database.EnsureCreated();
            }

            // Pass the ServiceProvider to the App class
            return new App(app.Services);
        }
    }
}
