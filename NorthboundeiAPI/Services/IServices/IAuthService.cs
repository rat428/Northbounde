using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.Services
{
    public interface IAuthService
    {
        Task<IEnumerable<ServiceAuthTbl>> GetAllServices();
    }
}