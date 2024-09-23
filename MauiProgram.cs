using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yes_Chef.Data;
using System.Reflection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;


namespace Yes_Chef
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Register DbContext
            builder.Services.AddDbContext<YesChefContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("YesChefConnection")));

            return builder.Build();
        }
    }
}