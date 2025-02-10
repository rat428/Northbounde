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
            UserAppTheme = AppTheme.Light;

            InitializeComponent();
            _splashScreen = serviceProvider.GetService<SplashScreenPage>()!;
            MainPage = _splashScreen;
        }
         protected async override void OnStart()
        {
            base.OnStart();
            await _splashScreen.OnStart();
        }
    }
}
