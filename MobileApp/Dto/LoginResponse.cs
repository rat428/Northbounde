using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Dto
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Warning { get; set; }
        public string Error { get; set; }
        public string Username { get; set; }
        public string Key { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}