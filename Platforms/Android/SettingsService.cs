using Android.Content;
using Android.Provider;
using Northboundei.Mobile.IServices;
using Application = Android.App.Application;
using AndroidNet = Android.Net;

namespace Northboundei.Mobile.Platforms.Android
{
    public class SettingsService : ISettingsService
    {
        public async Task OpenAirplaneModeSettings()
        {
            var intent = new Intent(Settings.ActionAirplaneModeSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);

        }

        public async Task CurrentAppSettingsAsync()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var intent = new Intent(Settings.ActionApplicationDetailsSettings);
                intent.SetData(AndroidNet.Uri.Parse("package:" + AppInfo.PackageName));
                intent.AddFlags(ActivityFlags.NewTask);
                Platform.CurrentActivity!.StartActivity(intent);
            }
        }
    }
}
