using Northboundei.Mobile.APIs;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;

namespace Northboundei.Mobile.Services
{
    public class ServiceAuthService : IServiceAuthService
    {
        private readonly ServiceAuthAPI _serviceAuth;

        public ServiceAuthService(ServiceAuthAPI service)
        {
            _serviceAuth = service;
        }

        public async Task<IEnumerable<ServiceAuthData>?> GetServiceAuthDataAsync(bool Offline = true)
        {
            try
            {
                var result = Offline ?
                    await DatabaseService.GetAllDataAsync<ServiceAuthData>() :
                    await _serviceAuth.GetServicesAsync();
                return result.Where(
                    c => c.ProviderNpi == SessionManager.UserInfo.NpiNo &&
                    DateTime.Now >= DateTime.Parse(c.ServiceAuthStart) &&
                    DateTime.Now <= DateTime.Parse(c.ServiceAuthEnd));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return null;
            }
        }

    }
}
