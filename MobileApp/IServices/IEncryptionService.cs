using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.IServices
{
    public interface IEncryptionService
    {
        Task<string> GetEncryptionKeyAsync(string key);
    }
}
