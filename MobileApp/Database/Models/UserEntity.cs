using Northboundei.Mobile.Dto;
using SQLite;

namespace Northboundei.Mobile.Database.Models
{
    public class UserEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string EncryptionKey { get; set; } = "DebugKey";
        /// <summary>
        /// Expiration time is UTC from the backend 
        /// </summary>
        public DateTime ExpirationTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool KeepMeLoggedIn { get; set; }
        public bool IsLoggedIn { get; set; }
        public string DeviceInfo { get; set; }

        public UserInfoData UserInfo { get; set; }
    }
}
