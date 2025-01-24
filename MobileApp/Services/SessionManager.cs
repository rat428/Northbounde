using Newtonsoft.Json;
using Northboundei.Mobile.Database.Models;
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
        static public bool IsNotesSync { get => GetSercure(nameof(IsNotesSync)); set => SetSercure(nameof(IsNotesSync), value); }

        static public bool IsAuthSync { get => GetSercure(nameof(IsAuthSync)); set => SetSercure(nameof(IsAuthSync), value); }
        static public bool IsUserSync { get => GetSercure(nameof(IsUserSync)); set => SetSercure(nameof(IsUserSync), value); }

        static public void ClearSession()
        {
            UserContext = new UserEntity();
            IsNotesSync = false;
            IsAuthSync = false;
            IsUserSync = false;
        }

        private static async void SetSercure(string v, bool value)
        {
            await SecureStorage.Default.SetAsync(SessionManager.UserContext.EncryptionKey + v, JsonConvert.SerializeObject(value)).ConfigureAwait(false);
        }

        private static bool GetSercure(string v)
        {

            var result = SecureStorage.Default.GetAsync(SessionManager.UserContext.EncryptionKey + v);

            result.Wait();
            if (result.Result is null)
            {
                return false;
            }
            return bool.Parse(result.Result);
        }

    }
}
