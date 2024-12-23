using Northboundei.Mobile.IServices;
using System.Net.Http.Headers;

namespace Northboundei.Mobile.APIs
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly Func<Task<string>> _getToken;

        public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
        {
            _getToken = getToken ?? throw new ArgumentNullException(nameof(getToken));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _getToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("Authorization", $"Bearer {token}");
            }

            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }


    public class AuthHttpDelegatingHandler : DelegatingHandler
    {
        readonly IUserService _userService;
        public AuthHttpDelegatingHandler(IUserService userService)
            => _userService = userService;


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_userService.AuthToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this._userService.AuthToken);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
