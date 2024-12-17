using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.Models
{
    public class SettingsModel : BaseModel
    {
        private bool? _locationSharingAllowed;
        private bool? _airplaneMode;
        private bool? _gpsOn;
        private bool? _developerToolsEnabled;
        private bool? _allowRunInBackground;
        private bool? _internetAccessAvailable;
        private bool? _autoTimeZone;
        private bool? _autoDateTime;

        public bool? LocationSharingAllowed
        {
            get => _locationSharingAllowed;
            set => SetProperty(ref _locationSharingAllowed, value);
        }

        public bool? AirplaneMode
        {
            get => _airplaneMode;
            set => SetProperty(ref _airplaneMode, value);
        }

        public bool? GPSOn
        {
            get => _gpsOn;
            set => SetProperty(ref _gpsOn, value);
        }

        public bool? DeveloperToolsEnabled
        {
            get => _developerToolsEnabled;
            set => SetProperty(ref _developerToolsEnabled, value);
        }

        public bool? AllowRunInBackground
        {
            get => _allowRunInBackground;
            set => SetProperty(ref _allowRunInBackground, value);
        }

        public bool? InternetAccessAvailable
        {
            get => _internetAccessAvailable;
            set => SetProperty(ref _internetAccessAvailable, value);
        }
        public bool? AutoDateTime
        {
            get => _autoDateTime;
            set => SetProperty(ref _autoDateTime, value);
        }
        public bool? AutoTimeZone
        {
            get => _autoTimeZone;
            set => SetProperty(ref _autoTimeZone, value);
        }
    }

}
