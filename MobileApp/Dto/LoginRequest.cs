using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Dto
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }
    }
}