using Northboundei.Mobile.Dto;
using Refit;

namespace Northboundei.Mobile.APIs
{
    public interface NoteAPI
    {
        [Post("/Api/SessionNote/Notes")]

        Task<IEnumerable<SessionNoteResponse>> GetNotesAsync();
    }

}
