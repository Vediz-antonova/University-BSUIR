using Antonava__353504.Application;
using Antonava__353504.Persistense;
using Antonava__353504.Persistense.Data;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Antonava__353504.UI;
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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        string settingsStream = "Antonava__353504.UI.appsettings.json";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(settingsStream);

        if (stream is null)
        {
            throw new FileNotFoundException($"Файл конфигурации '{settingsStream}' не найден в ресурсах.");
        }

        builder.Configuration.AddJsonStream(stream);

        // Получение строки подключения и формирование пути к базе данных
        var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
        var dataDirectory = FileSystem.Current.AppDataDirectory + "/";
        connStr = string.Format(connStr, dataDirectory);
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connStr)
            .Options;

        builder.Services
            .AddApplication()
            .AddPersistence(options)
            .RegisterPages()
            .RegisterViewModels();

        //DbInitializer.Initialize(builder.Services.BuildServiceProvider()).Wait();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
