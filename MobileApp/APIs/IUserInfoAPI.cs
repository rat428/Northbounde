using Northboundei.Mobile.Dto;
using Refit;

namespace Northboundei.Mobile.APIs;
public interface IUserInfoAPI
{
    [Get("api/Users")]
    Task<IEnumerable<UserInfoData>> GetUsersInfo();
}
