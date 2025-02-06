namespace NorthboundeiAPI.DTO
{
    public class ChangePasswordDto
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string TimeZone { get; set; }
    }
}
