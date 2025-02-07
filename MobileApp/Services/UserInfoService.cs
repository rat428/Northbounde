using Northboundei.Mobile.APIs;
using Northboundei.Mobile.Dto;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoAPI _userInfoAPI;

        public UserInfoService(IUserInfoAPI userInfoAPI)
        {
            _userInfoAPI = userInfoAPI;
        }

        public async Task<IEnumerable<UserInfoData>> GetUsersInfoAsync()
        {
            return await _userInfoAPI.GetUsersInfo();
        }
    }
}
