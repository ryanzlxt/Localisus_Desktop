using Microsoft.Extensions.Logging;
using Localimed.Services;

namespace Localimed
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
                    fonts.AddFont("Montserrat-Bold.ttf", "Montserrat");
                });

            builder.Services.AddHttpClient<MedicamentoApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7140/");
            });

            builder.Services.AddTransient<Views.VisualizarEstoque>();

            builder.Logging.AddDebug();


            return builder.Build();
        }
    }
}