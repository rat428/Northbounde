
using System.Threading.Tasks;

namespace Northboundei.Mobile.Services
{
    public interface IGeoLocationService
    {
        Task<string> GetGpsLocationAsync();
    }
}