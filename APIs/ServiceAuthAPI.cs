using Northboundei.Mobile.Dto;
using Refit;

namespace Northboundei.Mobile.APIs
{
    public interface ServiceAuthAPI
    {
        [Post("/Api/Authentication/Login")]

        Task<IEnumerable<ServiceAuthResponse>> GetServicesAsync();
    }

}
