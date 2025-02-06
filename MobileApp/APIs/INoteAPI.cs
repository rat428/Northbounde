using Northboundei.Mobile.Dto;
using Refit;

namespace Northboundei.Mobile.APIs
{
    public interface INoteAPI
    {
        [Get("/Api/SessionNote/Notes")]
        Task<IEnumerable<SessionNoteData>> GetNotesAsync();
    }

}
