﻿using Microsoft.Maui.ApplicationModel;
using Northboundei.Mobile.APIs;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Services
{
    public class UserService : IUserService
    {
        private readonly AuthAPI _myApi;
        public string AuthToken { get; set; } = string.Empty;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            var client = httpClientFactory.CreateClient(nameof(AuthAPI));
            client.Timeout = TimeSpan.FromSeconds(10);
            _myApi = RestService.For<AuthAPI>(client);
        }
        public async Task<LoginResponse> AuthAsync(string token)
        {
            return await _myApi.AuthAsync();
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest login)
        {
            var result= await _myApi.LoginAsync(login);
            AuthToken = result.Token;
            return result;
        }
    }
}
