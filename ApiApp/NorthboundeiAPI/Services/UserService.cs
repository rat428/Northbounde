using Microsoft.EntityFrameworkCore;
using NorthboundeiAPI.Database;
using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.Services
{
    public class UserService : IUserService
    {
        ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserInfoTbl>> GetUsersInfo()
        {
            var result = await _dbContext.UserInfoTbls.ToListAsync();
            return result;
        }
    }
}
