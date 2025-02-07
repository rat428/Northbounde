using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;

namespace Northboundei.Mobile.Services
{
    public static class GeoLocationService
    {
        public static async Task<string> GetGpsLocationAsync()
        {
            var location = await Geolocation.Default.GetLocationAsync();
            return location!.Latitude.ToString() + ", " + location.Longitude.ToString() + ';';
        }
    }
}
