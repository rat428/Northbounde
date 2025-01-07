using Northboundei.Mobile.APIs;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteAPI _noteAPI;

        public NoteService(INoteAPI noteAPI)
        {
            _noteAPI = noteAPI;
        }

 

        public async Task<IEnumerable<SessionNoteResponse>> GetNotesAsync()
        {
            try
            {
                var result = await _noteAPI.GetNotesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }



    }
}
