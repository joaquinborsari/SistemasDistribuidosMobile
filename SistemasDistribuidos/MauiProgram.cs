using Microsoft.Extensions.Logging;
using Maui.GoogleMaps.Hosting;

namespace SistemasDistribuidos;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
//#if ANDROID
//            .UseGoogleMaps()
//#elif IOS
//			.UseGoogleMaps("AIzaSyDF3UgkZusJXEUHRkyn2v9hDRZYIuqiJJc")
//#endif
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiMaps();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

