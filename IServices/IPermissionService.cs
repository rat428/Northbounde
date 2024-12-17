using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.IServices
{
    public interface IPermissionService
    {
        public event EventHandler<bool?> GpsStatusChanged;
        public event EventHandler<bool?> AirplaneModeChanged;
        public event EventHandler<bool?> TimeZoneChanged;
        public event EventHandler<bool?> AutoDateChanged;

        Task<bool?> IsLocationPermissionGrantedAsync();
        Task<bool?> IsAirplaneModeEnabled();
        Task<bool?> IsGpsEnabled();
        Task<bool?> IsDeveloperModeEnabled();
        Task<bool> IsInternetAvailable();
        Task<bool> CanRunInBackground();
    }
}
