using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.Services
{
    public interface ISessionNoteService
    {
        Task<IEnumerable<SessionNoteTbl>> GetSessionNotes();
    }
}