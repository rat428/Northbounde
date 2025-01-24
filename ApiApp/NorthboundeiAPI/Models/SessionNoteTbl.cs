using System;
using System.Collections.Generic;

namespace NorthboundeiAPI.Models;

public partial class SessionNoteTbl
{
    public string SessionId { get; set; } = null!;

    public DateOnly? SessionDate { get; set; }

    public string? SessionCpt1 { get; set; }

    public string? SessionCpt1Units { get; set; }

    public string? SessionCpt2 { get; set; }

    public string? SessionCpt2Units { get; set; }

    public string? SessionCpt3 { get; set; }

    public string? SessionCpt3Units { get; set; }

    public string? SessionCpt4 { get; set; }

    public string? SessionCpt4Units { get; set; }

    public string? SessionSection { get; set; }

    public string? EiNumber { get; set; }

    public string? Npi { get; set; }

    public string? ServiceType { get; set; }

    public string? ServiceLocation { get; set; }

    public DateOnly? DateNoteWritten { get; set; }

    public TimeOnly? TimeFrom { get; set; }

    public TimeOnly? TimeTo { get; set; }

    public string? Icd10Code { get; set; }

    public string? SessionCancelled { get; set; }

    public string? SessionMadeupBy { get; set; }

    public string? MakeUpSession { get; set; }

    public string? MakeUpFor { get; set; }

    public string? Participants { get; set; }

    public string? ParticipantOther { get; set; }

    public string? ParticipantUnavailble { get; set; }

    public string? Describe { get; set; }

    public string? Additional { get; set; }

    public string? Outcomes { get; set; }

    public string? Routine { get; set; }

    public string? Strategies { get; set; }

    public string? HowWork { get; set; }

    public string? Section5 { get; set; }

    public string? ParentImg { get; set; }

    public string? TherapistImg { get; set; }

    public string? ParentDate { get; set; }

    public string? TherapistDate { get; set; }

    public string? Relationship { get; set; }

    public string? ParentTimeStamp { get; set; }

    public string? TherapistTimeStamp { get; set; }

    public string? ParentGps { get; set; }

    public string? TherapistGps { get; set; }

    public string? CoVisitPresent { get; set; }

    public string? DraftFinal { get; set; }

    public string? ServiceAuthorization { get; set; }

    public string? AttendanceType { get; set; }

    public string? RoutineOther { get; set; }

    public string? StrategiesOther { get; set; }

    public string? HowWorkOther { get; set; }

    public string? RelationshipOther { get; set; }

    public string? SessionNoteType { get; set; }

    public string? Qa { get; set; }

    public string? ServiceLogImg { get; set; }

    public string? ServiceLogGps { get; set; }

    public DateTime? ServiceLogTimestamp { get; set; }

    public TimeOnly? TimeSpent { get; set; }

    public string? License { get; set; }
}
