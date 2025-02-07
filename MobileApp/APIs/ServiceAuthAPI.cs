using Northboundei.Mobile.Dto;
using Refit;

namespace Northboundei.Mobile.APIs
{
    public interface ServiceAuthAPI
    {
        [Get("/Api/Authentication/service-auth")]

        Task<IEnumerable<ChildData>> GetServicesAsync();
    }

}
