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

        public async Task<IEnumerable<ChildDataResponse>?> GetChildrenAsync()
        {
            try
            {
                var result = await _serviceAuth.GetServicesAsync();
                return result.Where(c => c.ProviderNpi == SessionManager.UserContext.UserInfo.NpiNo);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return null;
            }
        }

    }
}
