using System;
using System.Collections.Generic;

namespace NorthboundeiAPI.Models;

public partial class UserInfoTbl
{
    public string ProviderId { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Credentials { get; set; }

    public string? LicenseNo { get; set; }

    public string? NpiNo { get; set; }
}
