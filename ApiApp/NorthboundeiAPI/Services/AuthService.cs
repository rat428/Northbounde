using Microsoft.EntityFrameworkCore;
using NorthboundeiAPI.Database;
using NorthboundeiAPI.Models;
using System.Collections.Immutable;

namespace NorthboundeiAPI.Services
{
    public class AuthService : IAuthService
    {
        ApplicationDbContext _dbContext;
        public AuthService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceAuthTbl>> GetAllServices()
        {
            var result = await _dbContext.ServiceAuthTbls.ToListAsync();
            return result;
        }
    }
}
