using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext()
        {
        }

        public virtual DbSet<ServiceAuthTbl> ServiceAuthTbls { get; set; }

        public virtual DbSet<SessionNoteTbl> SessionNoteTbls { get; set; }

        public virtual DbSet<UserInfoTbl> UserInfoTbls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceAuthTbl>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("service_auth_tbl");

                entity.Property(e => e.AuthorizationStatus)
                    .HasMaxLength(50)
                    .HasColumnName("authorization_status");
                entity.Property(e => e.CaseReference)
                    .HasMaxLength(50)
                    .HasColumnName("case_reference");
                entity.Property(e => e.CoVisitAllowed)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("coVisitAllowed");
                entity.Property(e => e.CovisitsRemaining).HasColumnName("covisitsRemaining");
                entity.Property(e => e.DxCode).HasColumnName("dxCode");
                entity.Property(e => e.DxCode2).HasColumnName("dxCode2");
                entity.Property(e => e.DxCode3).HasColumnName("dxCode3");
                entity.Property(e => e.DxCode4).HasColumnName("dxCode4");
                entity.Property(e => e.DxCode5).HasColumnName("dxCode5");
                entity.Property(e => e.EndDate).HasColumnName("end_date");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");
                entity.Property(e => e.MakeUpsRemaining).HasColumnName("makeUpsRemaining");
                entity.Property(e => e.MakeupVisitAllowed)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("makeupVisitAllowed");
                entity.Property(e => e.MinutesPerSession)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("minutes_per_session");
                entity.Property(e => e.Municipality)
                    .HasMaxLength(50)
                    .HasColumnName("municipality");
                entity.Property(e => e.NumberCoVisits).HasColumnName("numberCoVisits");
                entity.Property(e => e.NumberMakeUps).HasColumnName("numberMakeUps");
                entity.Property(e => e.NumberVisitsRemaining)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("numberVisitsRemaining");
                entity.Property(e => e.NyeisId).HasColumnName("nyeis_id");
                entity.Property(e => e.Objectives).HasColumnName("objectives");
                entity.Property(e => e.Objectives2).HasColumnName("objectives2");
                entity.Property(e => e.Objectives3).HasColumnName("objectives3");
                entity.Property(e => e.Outcomes).HasColumnName("outcomes");
                entity.Property(e => e.Outcomes2).HasColumnName("outcomes2");
                entity.Property(e => e.Outcomes3).HasColumnName("outcomes3");
                entity.Property(e => e.PerWm)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("perWM");
                entity.Property(e => e.ProviderNpi)
                    .HasMaxLength(50)
                    .HasColumnName("provider_npi");
                entity.Property(e => e.ServiceAuthEnd).HasColumnName("serviceAuthEnd");
                entity.Property(e => e.ServiceAuthStart).HasColumnName("serviceAuthStart");
                entity.Property(e => e.ServiceAuthorization).HasColumnName("service_authorization");
                entity.Property(e => e.ServiceType).HasColumnName("service_type");
                entity.Property(e => e.StartDate).HasColumnName("start_date");
                entity.Property(e => e.UnitsApproved)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("units_approved");
                entity.Property(e => e.UnitsAuthorized)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("units_authorized");
            });

            modelBuilder.Entity<SessionNoteTbl>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("session_note_tbl");

                entity.Property(e => e.Additional).HasColumnName("additional");
                entity.Property(e => e.AttendanceType)
                    .HasMaxLength(50)
                    .HasColumnName("Attendance_type");
                entity.Property(e => e.CoVisitPresent).HasColumnName("CoVisit_present");
                entity.Property(e => e.DateNoteWritten).HasColumnName("date_note_written");
                entity.Property(e => e.Describe).HasColumnName("describe");
                entity.Property(e => e.DraftFinal).HasColumnName("Draft_Final");
                entity.Property(e => e.EiNumber)
                    .HasMaxLength(50)
                    .HasColumnName("EI_number");
                entity.Property(e => e.HowWork).HasColumnName("how_work");
                entity.Property(e => e.HowWorkOther).HasColumnName("How_work_other");
                entity.Property(e => e.Icd10Code).HasColumnName("ICD10_code");
                entity.Property(e => e.MakeUpFor)
                    .HasMaxLength(50)
                    .HasColumnName("make_up_for");
                entity.Property(e => e.MakeUpSession)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("make_up_session");
                entity.Property(e => e.Npi).HasColumnName("NPI");
                entity.Property(e => e.Outcomes).HasColumnName("outcomes");
                entity.Property(e => e.ParentDate).HasColumnName("parent_date");
                entity.Property(e => e.ParentGps).HasColumnName("parent_gps");
                entity.Property(e => e.ParentImg).HasColumnName("parent_img");
                entity.Property(e => e.ParentTimeStamp).HasColumnName("parent_time_stamp");
                entity.Property(e => e.ParticipantOther).HasColumnName("participant_other");
                entity.Property(e => e.ParticipantUnavailble).HasColumnName("participant_unavailble");
                entity.Property(e => e.Participants)
                    .HasMaxLength(50)
                    .HasColumnName("participants");
                entity.Property(e => e.Qa).HasColumnName("QA");
                entity.Property(e => e.Relationship)
                    .HasMaxLength(50)
                    .HasColumnName("relationship");
                entity.Property(e => e.RelationshipOther).HasColumnName("Relationship_other");
                entity.Property(e => e.Routine).HasColumnName("routine");
                entity.Property(e => e.RoutineOther).HasColumnName("Routine_other");
                entity.Property(e => e.Section5).HasColumnName("section_5");
                entity.Property(e => e.ServiceAuthorization)
                    .HasMaxLength(50)
                    .HasColumnName("Service_authorization");
                entity.Property(e => e.ServiceLocation)
                    .HasMaxLength(50)
                    .HasColumnName("service_location");
                entity.Property(e => e.ServiceLogGps).HasColumnName("service_log_gps");
                entity.Property(e => e.ServiceLogImg).HasColumnName("service_log_img");
                entity.Property(e => e.ServiceLogTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("service_log_timestamp");
                entity.Property(e => e.ServiceType)
                    .HasMaxLength(50)
                    .HasColumnName("service_type");
                entity.Property(e => e.SessionCancelled)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("session_cancelled");
                entity.Property(e => e.SessionCpt1).HasColumnName("session_cpt1");
                entity.Property(e => e.SessionCpt1Units)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("session_cpt1_units");
                entity.Property(e => e.SessionCpt2).HasColumnName("session_cpt2");
                entity.Property(e => e.SessionCpt2Units)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("session_cpt2_units");
                entity.Property(e => e.SessionCpt3).HasColumnName("session_cpt3");
                entity.Property(e => e.SessionCpt3Units)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("session_cpt3_units");
                entity.Property(e => e.SessionCpt4).HasColumnName("session_cpt4");
                entity.Property(e => e.SessionCpt4Units)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("session_cpt4_units");
                entity.Property(e => e.SessionDate).HasColumnName("session_date");
                entity.Property(e => e.SessionId).HasColumnName("sessionID");
                entity.Property(e => e.SessionMadeupBy).HasColumnName("session_madeup_by");
                entity.Property(e => e.SessionNoteType)
                    .HasMaxLength(50)
                    .HasColumnName("sessionNote_type");
                entity.Property(e => e.SessionSection).HasColumnName("session_section");
                entity.Property(e => e.Strategies).HasColumnName("strategies");
                entity.Property(e => e.StrategiesOther).HasColumnName("Strategies_other");
                entity.Property(e => e.TherapistDate).HasColumnName("therapist_date");
                entity.Property(e => e.TherapistGps).HasColumnName("Therapist_gps");
                entity.Property(e => e.TherapistImg).HasColumnName("therapist_img");
                entity.Property(e => e.TherapistTimeStamp).HasColumnName("therapist_time_stamp");
                entity.Property(e => e.TimeFrom).HasColumnName("time_from");
                entity.Property(e => e.TimeTo).HasColumnName("time_to");
            });

            modelBuilder.Entity<UserInfoTbl>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("user_info_tbl");

                entity.Property(e => e.Credentials).HasMaxLength(50);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");
                entity.Property(e => e.LicenseNo)
                    .HasMaxLength(50)
                    .HasColumnName("license_no");
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("middle_name");
                entity.Property(e => e.NpiNo)
                    .HasMaxLength(50)
                    .HasColumnName("npi_no");
                entity.Property(e => e.ProviderId)
                    .HasMaxLength(50)
                    .HasColumnName("provider_id");
            });
            base.OnModelCreating(modelBuilder);

        }


    }
}
