using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace _353504_Antonava_Lab4.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

#if WINDOWS
        const int windowWidth = 450;
        const int windowHeight = 885;

        var mauiWindow = Application.Windows[0].Handler.PlatformView as Microsoft.UI.Xaml.Window;
        if (mauiWindow != null)
        {
            var appWindow = AppWindow.GetFromWindowId(
                Win32Interop.GetWindowIdFromWindow(
                    WinRT.Interop.WindowNative.GetWindowHandle(mauiWindow)
                )
            );

            if (appWindow != null)
            {
                appWindow.Resize(new SizeInt32(windowWidth, windowHeight));
            }
        }
#endif
    }
}

