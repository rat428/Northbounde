using Northboundei.Mobile.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Services
{
    public class PermissionService : IPermissionService
    {
        public event EventHandler<bool?> GpsStatusChanged;
        public event EventHandler<bool?> AirplaneModeChanged;
        public event EventHandler<bool?> TimeZoneChanged;
        public event EventHandler<bool?> AutoDateChanged;

        public Task<bool> CanRunInBackground()
        {
            bool result = false;
            return Task.FromResult(result);
        }

        public Task<bool?> IsAirplaneModeEnabled()
        {
            bool? result = null;
            return Task.FromResult(result);
        }

        public Task<bool?> IsDeveloperModeEnabled()
        {
            bool? result = null;
            return Task.FromResult(result);
        }

        public Task<bool?> IsGpsEnabled()
        {
            bool? result = null;
            return Task.FromResult(result);
        }

        public Task<bool> IsInternetAvailable()
        {
            bool result = false;
            return Task.FromResult(result);
        }

        public Task<bool?> IsLocationPermissionGrantedAsync()
        {
            bool? result = null;
            return Task.FromResult(result);
        }
    }
}
