using System.Xml.Linq;

namespace Northboundei.Mobile.Dto
{
    public class ChildData
    {
        public string? NyeisId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Municipality { get; set; }

        public string? CaseReference { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Outcomes { get; set; }

        public string? Objectives { get; set; }

        public string? Outcomes2 { get; set; }

        public string? Objectives2 { get; set; }

        public string? Outcomes3 { get; set; }

        public string? Objectives3 { get; set; }

        public string? ServiceAuthorization { get; set; }

        public DateOnly? ServiceAuthStart { get; set; }

        public DateOnly? ServiceAuthEnd { get; set; }

        public string? AuthorizationStatus { get; set; }

        public string? ServiceType { get; set; }

        public string? UnitsApproved { get; set; }

        public string? NumberVisitsRemaining { get; set; }

        public string? PerWm { get; set; }

        public string? MinutesPerSession { get; set; }

        public string? UnitsAuthorized { get; set; }

        public string? MakeupVisitAllowed { get; set; }

        public int? NumberMakeUps { get; set; }

        public int? MakeUpsRemaining { get; set; }

        public string? CoVisitAllowed { get; set; }

        public int? CovisitsRemaining { get; set; }

        public int? NumberCoVisits { get; set; }

        public string? ProviderNpi { get; set; }

        public string? DxCode { get; set; }

        public string? DxCode2 { get; set; }

        public string? DxCode3 { get; set; }

        public string? DxCode4 { get; set; }

        public string? DxCode5 { get; set; }
        public string DisplayName { get => $"{FirstName} {LastName}"; }
        
    }

}