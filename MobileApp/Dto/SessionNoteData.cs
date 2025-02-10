namespace Northboundei.Mobile.Dto
{
    public class SessionNoteData
    {
        /// <summary>
        /// SessionID (hiddenfield)
        /// nyeis_id-service_authorization-session_date-Date.Now.toString("yyyyMMddHHmmsstttt")-"AppV1"
        /// </summary>
        public string SessionId { get; set; } = null!;

        /// <summary>
        /// Session date
        /// yyyy-MM-dd
        /// </summary>
        public string? SessionDate { get; set; }

        /// <summary>
        /// CPT Codes/Units CheckBoxList
        /// "Selected CPTCode" : "Selected Units" ; "Selected CPTCode" : "Selected Units" ; "Selected CPTCode" : "Selected Units"...
        /// </summary>
        public string? SessionCpt1 { get; set; }

        public string? SessionCpt1Units { get; set; }

        public string? SessionCpt2 { get; set; }

        public string? SessionCpt2Units { get; set; }

        public string? SessionCpt3 { get; set; }

        public string? SessionCpt3Units { get; set; }

        public string? SessionCpt4 { get; set; }

        public string? SessionCpt4Units { get; set; }

        /// <summary>
        /// 
        public string? SessionSection { get; set; }

        /// <summary>
        /// Attendance Type Dropdown
        /// Attendance type text
        /// </summary>
        public string? AttendanceType { get; set; }

        /// <summary>
        /// Childs Name dropdown
        /// Value of Childs Name Dropdown Selection
        /// </summary>
        public string? EiNumber { get; set; }

        /// <summary>
        /// From the user data that is originally obtained from the "user_info_tbl"
        /// </summary>
        public string? Npi { get; set; }

        /// <summary>
        /// Service type dropdown
        /// Text of selection
        /// </summary>
        public string? ServiceType { get; set; }

        /// <summary>
        /// Service type dropdown
        /// Value of selection
        /// </summary>
        public string? ServiceAuthorization { get; set; }

        /// <summary>
        /// Service Location checkbox list
        /// Text value
        /// </summary>
        public string? ServiceLocation { get; set; }

        /// <summary>
        /// Date the note was written
        /// Date.now.tostring("yyyy-MM-dd")
        /// </summary>
        public string? DateNoteWritten { get; set; }

        /// <summary>
        /// Start time Text
        /// HH:mm
        /// </summary>
        public string? TimeFrom { get; set; }

        /// <summary>
        /// End time Text
        /// HH:mm
        /// </summary>
        public string? TimeTo { get; set; }

        /// <summary>
        /// ICD10 CheckBoxList
        /// "Selected ICD10";"Selected ICD10";"Selected ICD10"…
        /// </summary>
        public string? Icd10Code { get; set; }

        /// <summary>
        /// Attendance Type Dropdown
        /// If "Missed" then "Yes" else "No"
        /// </summary>
        public string? SessionCancelled { get; set; }

        /// <summary>
        /// Attendance Type Dropdown / Session Date Text
        /// If "Missed" then "Session Date + 14 days", else null
        /// </summary>
        public string? SessionMadeupBy { get; set; }

        /// <summary>
        /// Attedance Type
        /// If "Make-up" then "Yes" else "No"
        /// </summary>
        public string? MakeUpSession { get; set; }

        /// <summary>
        /// Attendance Type / Make-up date dropdown
        /// If "Make-up" then make-up date selected ("yyyy-MM-dd") else null
        /// </summary>
        public string? MakeUpFor { get; set; }

        /// <summary>
        /// Participants CheckBoxList
        /// Selected value;Selected value;Selected value…
        /// </summary>
        public string? Participants { get; set; }

        /// <summary>
        /// Participants CheckBoxList
        /// If "Other" then "How did you communicate with the Caregiver" textbox, else Null
        /// </summary>
        public string? ParticipantUnavailble { get; set; }

        /// <summary>
        /// Attendance Type Dropdown / Covisit partipants Text Box
        /// if Attendance Type value = "CV3" then "Covist Participants" text, else Null
        /// </summary>
        public string? CoVisitPresent { get; set; }

        /// <summary>
        /// Describe Text box
        /// Text from Describe TextBox
        /// </summary>
        public string? Describe { get; set; }

        public string? Additional { get; set; }

        /// <summary>
        /// Outcomes/Objectives Gridview
        /// @selected Outcome~selected objective~selected objective~selected objective@selected Outcome~selected objective~selected objective~selected objective…
        /// </summary>
        public string? Outcomes { get; set; }

        public string? Routine { get; set; }

        public string? Strategies { get; set; }

        /// <summary>
        /// Collaboration Checkboxlist / Collaboration Other Text
        /// Selected value;Selected value;Selected value… If Collaboration CBL "Other" selected, then Collaboration Other textbox Text
        /// </summary>
        public string? HowWork { get; set; }

        /// <summary>
        /// Carry Over Text Box
        /// Carry over text
        /// </summary>
        public string? Section6 { get; set; }

        /// <summary>
        /// Parent Signature in Base64
        /// data:image/png;Base64…
        /// </summary>
        public string? ParentImg { get; set; }

        /// <summary>
        /// Therapist Signature in Base64
        /// data:image/png;Base64…
        /// </summary>
        public string? TherapistImg { get; set; }

        /// <summary>
        /// Parent end signature Submit Btn
        /// Date.now.tostring("MM-dd-yyyy HH:mm:ss tt ")
        /// </summary>
        public string? ParentDate { get; set; }

        /// <summary>
        /// Therapist end signature Submit Btn
        /// Date.now.tostring("MM-dd-yyyy HH:mm:ss tt ")
        /// </summary>
        public string? TherapistDate { get; set; }

        /// <summary>
        /// Reletionship dropdown within 2nd party signature page
        /// selected text or Other Textbox if Other textbox selected
        /// </summary>
        public string? Relationship { get; set; }

        /// <summary>
        /// Parent end signature Submit Btn
        /// Date.now.tostring("MM-dd-yyyy HH:mm:ss tt ")
        /// </summary>
        public string? ParentTimeStamp { get; set; }

        /// <summary>
        /// Therapist end signature Submit Btn
        /// Date.now.tostring("MM-dd-yyyy HH:mm:ss tt ")
        /// </summary>
        public string? TherapistTimeStamp { get; set; }

        /// <summary>
        /// Parent end signature Submit Btn
        /// Longitude,Latitude
        /// </summary>
        public string? ParentGps { get; set; }

        /// <summary>
        /// Therapist end signature Submit Btn
        /// Longitude,Latitude
        /// </summary>
        public string? TherapistGps { get; set; }

        /// <summary>
        /// Submit Buttons
        /// "Draft" or "Final"
        /// </summary>
        public string? DraftFinal { get; set; }

        public string? RoutineOther { get; set; }

        public string? StrategiesOther { get; set; }

        public string? HowWorkOther { get; set; }

        /// <summary>
        /// App
        /// </summary>
        public string? SessionNoteType { get; set; }

        public string? Qa { get; set; }

        /// <summary>
        /// Attendence signature Submit Btn
        /// data:image/png;Base64…
        /// </summary>
        public string? ServiceLogImg { get; set; }

        /// <summary>
        /// Attendence signature Submit Btn
        /// Longitude,Latitude
        /// </summary>
        public string? ServiceLogGps { get; set; }

        /// <summary>
        /// Attendence signature Submit Btn
        /// Date.now.tostring("MM-dd-yyyy HH:mm:ss tt ")
        /// </summary>
        public DateTime? ServiceLogTimestamp { get; set; }

        public string? TimeSpent { get; set; }

        /// <summary>
        /// From the user data that is originally obtained from the "user_info_tbl"
        /// </summary>
        public string? License { get; set; }

    }

}