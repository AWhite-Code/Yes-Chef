using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yes_Chef.Data;
using System.Reflection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Yes_Chef.ViewModels;
using Yes_Chef.Views;


namespace Yes_Chef
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Configure fonts and app
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Configure DbContext with SQLite
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "YesChefDatabase.db3");

            builder.Services.AddDbContext<YesChefContext>(options =>
            {
                options.UseSqlite($"Filename={dbPath}");
            });

            // Register services and view models
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<RecipeListPage>();
            builder.Services.AddTransient<RecipeListViewModel>();

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