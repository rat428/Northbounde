using Newtonsoft.Json;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Services
{
    static public class SessionManager
    {
        static public UserEntity UserContext { get; set; } = new UserEntity();
        static public UserInfoData UserInfo { get => GetSecure<UserInfoData>(nameof(UserInfo)); set => SetSecure(nameof(UserInfo), value); }
        static public bool IsNotesSync { get => GetSecure<bool>(nameof(IsNotesSync)); set => SetSecure(nameof(IsNotesSync), value); }

        static public bool IsAuthSync { get => GetSecure<bool>(nameof(IsAuthSync)); set => SetSecure(nameof(IsAuthSync), value); }
        static public bool IsUserSync { get => GetSecure<bool>(nameof(IsUserSync)); set => SetSecure(nameof(IsUserSync), value); }

        static public void ClearSession()
        {
            UserContext = new UserEntity();
            IsNotesSync = false;
            IsAuthSync = false;
            IsUserSync = false;
        }

        private static async void SetSecure(string v, object value)
        {
            await SecureStorage.Default.SetAsync(SessionManager.UserContext.EncryptionKey + v, JsonConvert.SerializeObject(value)).ConfigureAwait(false);
        }

        private static T GetSecure<T>(string v)
        {

            var result = SecureStorage.Default.GetAsync(SessionManager.UserContext.EncryptionKey + v);

            result.Wait();
            if (result.Result is null)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(result.Result);
        }
    }
}
