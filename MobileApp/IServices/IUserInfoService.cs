using Northboundei.Mobile.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Northboundei.Mobile.Services
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfoData>> GetUsersInfoAsync();
    }
}
