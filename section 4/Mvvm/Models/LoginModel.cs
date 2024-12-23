using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.Models
{
    public class LoginModel : BaseModel
    {
        string _userName;
        string _password;
        string _token;
        DateTime _expirationTime;

        public string UserName { get => _userName; set => SetProperty(ref _userName, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string Token { get => _token; set => SetProperty(ref _token, value); }
        public DateTime ExpirationTime { get => _expirationTime; set => SetProperty(ref _expirationTime, value); }
    }
}
