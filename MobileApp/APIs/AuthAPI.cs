using Northboundei.Mobile.Dto;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.APIs
{
    public interface AuthAPI
    {
        [Post("/Api/Authentication/Login")]

        Task<LoginResponse> LoginAsync(LoginRequest login);

        [Get("/Api/Authentication/auth")]
        Task<LoginResponse> AuthAsync();
    }
}
