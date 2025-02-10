using Northboundei.Mobile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.IServices
{
    public interface IServiceAuthService
    {
        Task<IEnumerable<ServiceAuthData>?> GetServiceAuthDataAsync(bool Offline = true);
    }
}
