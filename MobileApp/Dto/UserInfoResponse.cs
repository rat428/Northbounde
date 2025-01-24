namespace Northboundei.Mobile.Dto
{
    public partial class UserInfoResponse
    {
        public string ProviderId { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Credentials { get; set; }

        public string? LicenseNo { get; set; }

        public string? NpiNo { get; set; }
    }
}