using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using Microsoft.UI.Windowing;

namespace _353504_Antonava_Lab3.WinUI;

public partial class App : MauiWinUIApplication
{
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

