using Northboundei.Mobile.Dto;
using Refit;

namespace Northboundei.Mobile.APIs;
public interface IUserInfoAPI
{
    [Get("/Api/UserInfo/Users")]
    Task<IEnumerable<UserInfoData>> GetUsersInfo();
}
