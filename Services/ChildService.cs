using Northboundei.Mobile.APIs;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;

namespace Northboundei.Mobile.Services
{
    public class ChildService : IChildService
    {
        private readonly ServiceAuthAPI _serviceAuth;

        public ChildService(ServiceAuthAPI service)
        {
            _serviceAuth = service;
        }

        public async Task<IEnumerable<ServiceAuthResponse>> GetChildrenAsync()
        {
            try
            {
                var result = await _serviceAuth.GetServicesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
