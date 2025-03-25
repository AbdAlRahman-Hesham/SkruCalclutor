using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Contexts.Data;
using Contexts.Repos;
namespace SkruCalclutor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db");
            builder.Services.AddSingleton<ISavePlayerRepo>(provider =>
            {
                return new SavePlayerRepo(dbPath);
            });
            //builder.Services.AddSingleton<GameStateService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            

            return app;
        }
    }
}
