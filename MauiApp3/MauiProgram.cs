using MauiApp3.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MauiApp3 // Asigură-te că numele namespace-ului este corect
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            // Define the database task path
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tasks.db");
            builder.Services.AddSingleton<TaskDatabase>(new TaskDatabase(dbPath));

            //var dbPathDetail = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Details.db");
            //builder.Services.AddSingleton<DetailDatabase>(new DetailDatabase(dbPathDetail));

            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<DetailViewModel>();

            return builder.Build();
        }
    }
}
