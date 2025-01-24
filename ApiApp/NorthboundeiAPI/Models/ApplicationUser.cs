using Microsoft.AspNetCore.Identity;

namespace NorthboundeiAPI.Models
{
    public class ApplicationUser: IdentityUser
    {
        public DateTime PasswordExpirationDate { get; set; }
        public string TimeZone { get; set; }
        public DateTime? LastLogin { get; set; }
        public string UserStatus { get; set; } = "Active"; 
        public bool IsOnline { get; set; } 
        public DateTime? ExpirationDate { get; set; }

    }
}
