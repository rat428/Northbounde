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
        static public UserInfoData UserInfo { get => GetSecure<UserInfoData>(UserInfoKey); set => SetSecure(UserInfoKey, value); }

        static string UserInfoKey => $"{UserContext.EncryptionKey}-{nameof(UserInfo)}";

        static public void ClearSession()
        {
            UserContext = new UserEntity();
            SecureStorage.Default.RemoveAll();
        }

        private static async void SetSecure(string v, object value)
        {
            await SecureStorage.Default.SetAsync(v, JsonConvert.SerializeObject(value)).ConfigureAwait(false);
        }

        private static T GetSecure<T>(string v) where T : class, new()
        {
            var result = SecureStorage.Default.GetAsync(v);
            result.Wait();
            if (result.Result is null)
            {
                return new T();
            }
            return JsonConvert.DeserializeObject<T>(result.Result)!;
        }
    }
}
