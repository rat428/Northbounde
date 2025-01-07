using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.DependencyInjection;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Mvvm.ViewModels;
using Northboundei.Mobile.Mvvm.Views;

namespace Northboundei.Mobile
{
    public partial class App : Application
    {
        SplashScreenPage _splashScreen;
        public App(IServiceProvider serviceProvider)
        {
            Current.UserAppTheme = AppTheme.Light;

            InitializeComponent();
            _splashScreen = serviceProvider.GetService<SplashScreenPage>();
            MainPage = _splashScreen;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpR2VGfV5ycEVEalhVTnRWUj0eQnxTdEFiW35XcXBRQmJVWUFxXA==");
        }
         protected async override void OnStart()
        {
            base.OnStart();
            await _splashScreen.OnStart();
        }
    }
}
