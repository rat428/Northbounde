using Android.App.Usage;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Provider;
using Northboundei.Mobile.IServices;
using Application = Android.App.Application;

namespace Northboundei.Mobile
{
    public class PermissionService : BroadcastReceiver, IPermissionService
    {
        private GpsStatus.IListener? myAction;
        private const int REQUEST_CODE_LOCATION_PERMISSION = 1;
        private LocationManager _locationManager;
        public event EventHandler<bool?> GpsStatusChanged;
        public event EventHandler<bool?> AirplaneModeChanged;
        public event EventHandler<bool?> TimeZoneChanged;
        public event EventHandler<bool?> AutoDateChanged;



        public PermissionService()
        {
            var filter = new IntentFilter(Intent.ActionAirplaneModeChanged);
            Application.Context.RegisterReceiver(this, filter);
            _locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.LocationService);
        }
        public bool IsAirplaneModeOn()
        {
            return Settings.Global.GetInt(Android.App.Application.Context.ContentResolver, Settings.Global.AirplaneModeOn, 0) == 1;
        }

        public async Task<bool?> IsGpsEnabled()
        {
            return _locationManager.IsProviderEnabled(LocationManager.GpsProvider);
        }

        private void OnGpsStatusChanged(object? sender, bool isEnabled)
        {
            GpsStatusChanged?.Invoke(this, isEnabled);
        }

        public async Task<bool?> IsAirplaneModeEnabled()
        {
            var airplaneMode = Android.Provider.Settings.Global.GetInt(
               Android.App.Application.Context.ContentResolver,
               Android.Provider.Settings.Global.AirplaneModeOn, 0);
            return airplaneMode != 0;
        }

        public async Task<bool?> IsDeveloperModeEnabled()
        {
            var devOptions = Android.Provider.Settings.Global.GetInt(
                Android.App.Application.Context.ContentResolver,
                Android.Provider.Settings.Global.DevelopmentSettingsEnabled, 0);
            return devOptions != 0;
        }

        public async Task<bool> IsInternetAvailable()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        public async Task<bool?> IsLocationPermissionGrantedAsync()
        {
            var fineLocationStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            return fineLocationStatus == PermissionStatus.Granted;
        }


        public async Task<bool> CanRunInBackground()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var usageStatsManager = (UsageStatsManager)Android.App.Application.Context.GetSystemService(Context.UsageStatsService);
                var currentTime = System.DateTime.Now.Ticks;
                var stats = usageStatsManager.QueryUsageStats(UsageStatsInterval.Daily, currentTime - 1000 * 1000, currentTime);
                var recentStats = stats?.OrderByDescending(u => u.LastTimeUsed)?.FirstOrDefault();

                if (recentStats != null)
                {
                    var isAppInForeground = recentStats.PackageName == Android.App.Application.Context.PackageName;
                    return !isAppInForeground;
                }
            }

            return true;
        }

        public override async void OnReceive(Context context, Intent intent)
        {
            bool isAirplaneModeOn = IsAirplaneModeOn();
            AirplaneModeChanged?.Invoke(this, isAirplaneModeOn);
            bool isDateAuto = IsAutomaticDateTimeEnabled(context);
            bool isTimeZoneAuto = IsAutomaticTimeZoneEnabled(context);
            if(!isDateAuto)
                AutoDateChanged?.Invoke(this, isDateAuto);
            if (!isTimeZoneAuto)
                TimeZoneChanged?.Invoke(this, isTimeZoneAuto);

            if (intent.Action == LocationManager.GpsProvider)
            {
                bool? isGpsEnabled = await IsGpsEnabled();
                GpsStatusChanged?.Invoke(this, isTimeZoneAuto);
            }
        }

        public bool IsAutomaticDateTimeEnabled(Context context)
        {
            try
            {
                int autoTime = Settings.Global.GetInt(context.ContentResolver, Settings.Global.AutoTime);
                return autoTime == 1; // 1 indicates the setting is enabled
            }
            catch (Settings.SettingNotFoundException)
            {
                // Handle exception if the setting is not found
                return false;
            }
        }


        public bool IsAutomaticTimeZoneEnabled(Context context)
        {
            try
            {
                int autoTimeZone = Settings.Global.GetInt(context.ContentResolver, Settings.Global.AutoTime);
                return autoTimeZone == 1; // 1 indicates the setting is enabled
            }
            catch (Settings.SettingNotFoundException)
            {
                // Handle exception if the setting is not found
                return false;
            }
        }

    }

    public class GnssStatusCallback : GnssStatus.Callback
    {
        public event EventHandler<bool> GpsStatusChanged;
        bool _enabled;
        public GnssStatusCallback()
        {
        }
        public override void OnStarted()
        {
            _enabled = true;
            RaiseGpsStatusChangedEvent();
        }

        public override void OnStopped()
        {
            _enabled = false;
            RaiseGpsStatusChangedEvent();
        }

        private void RaiseGpsStatusChangedEvent()
        {
            GpsStatusChanged?.Invoke(this, _enabled);
        }
    }



}
