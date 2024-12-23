using Northboundei.Mobile.Dto;
using Refit;

namespace Northboundei.Mobile.APIs
{
    public interface IUserInfoAPI
    {
        [Post("/Api/Authentication/Login")]

        Task<IEnumerable<UserInfoResponse>> GetUserInfoAPI();

        [Get("/Api/Authentication/GenerateKey")]
        Task<IEnumerable<string>> GetEncryptionKey();
    }

}
