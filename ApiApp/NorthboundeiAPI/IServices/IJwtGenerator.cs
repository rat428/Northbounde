using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.IServices
{
    public interface IJwtGenerator
    {
        string GenerateToken(ApplicationUser user);
        public string GenerateSecureKey();
    }
}
