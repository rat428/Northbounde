using Northboundei.Mobile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.IServices
{
    public interface INoteService
    {
        Task<IEnumerable<SessionNoteData>> GetAllDrafts();
        Task<IEnumerable<SessionNoteData>> GetNotesAsync(bool Offline = true);
        Task<int> GetTotalCount();
    }
}
