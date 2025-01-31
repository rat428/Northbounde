using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.Models
{
    public partial class SettingsModel : ObservableObject
    {
        [ObservableProperty]
        private bool? _locationSharingAllowed;
        [ObservableProperty]
        private bool? _airplaneMode;
        [ObservableProperty]
        private bool? _gpsOn;
        [ObservableProperty]
        private bool? _developerToolsEnabled;
        [ObservableProperty]
        private bool? _allowRunInBackground;
        [ObservableProperty]
        private bool? _internetAccessAvailable;
        [ObservableProperty]
        private bool? _autoTimeZone;
        [ObservableProperty]
        private bool? _autoDateTime;
    }
}
