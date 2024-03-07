using App.Presentations.ViewModels;
using App.Presentations.Views;
using App.Repositories;
using App.Utils;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace App
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
                })
                .UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                        if(winuiAppWindow.Presenter is OverlappedPresenter p)
                        {
                            p.Maximize();
                        }                            
                    });
                });
            });
#endif
            builder.Services.AddSingleton<IClientRepository, ClientRepository>();
            builder.Services.AddSingleton<IAlertService, AlertService>();

            builder.Services.AddTransient<ClientListViewModel>();
            builder.Services.AddTransient<ClientAddViewModel>();
            builder.Services.AddTransient<ClientEditViewModel>();

            builder.Services.AddTransient<ClientListView>();
            builder.Services.AddTransient<ClientAddView>();
            builder.Services.AddTransient<ClientEditView>();

            return builder.Build();
        }
    }
}
