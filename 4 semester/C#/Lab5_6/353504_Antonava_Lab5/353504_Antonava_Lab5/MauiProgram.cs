using Microsoft.Extensions.Logging;
using _353504_Antonava_Lab5.Services;
namespace _353504_Antonava_Lab5;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder.Services.AddTransient<IDbService, SQLiteService>();
        builder.Services.AddTransient<BrigadePage>();
        builder.Services.AddHttpClient<IRateService, RateService>(client =>
        {
            client.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates/");
        });

        builder.Services.AddTransient<CurrencyConverterPage>();
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

		return builder.Build();
	}
}
