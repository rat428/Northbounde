using Microsoft.EntityFrameworkCore;
using NorthboundeiAPI.Database;
using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.Services
{
    public class SessionNoteService : ISessionNoteService
    {
        ApplicationDbContext _dbContext;
        public SessionNoteService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SessionNoteTbl>> GetSessionNotes()
        {
            var result= await _dbContext.SessionNoteTbls.ToListAsync();
            return result;
        }
    }
}
