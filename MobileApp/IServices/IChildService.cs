using Northboundei.Mobile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.IServices
{
    public interface IChildService
    {
        Task<IEnumerable<ChildDataResponse>?> GetChildrenAsync();
    }
}
