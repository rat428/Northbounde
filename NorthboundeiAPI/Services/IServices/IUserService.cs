using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoTbl>> GetUsersInfo();
    }
}