using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthboundeiAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "service_auth_tbl",
                columns: table => new
                {
                    nyeis_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    municipality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    case_reference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    outcomes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    objectives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    outcomes2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    objectives2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    outcomes3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    objectives3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    service_authorization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serviceAuthStart = table.Column<DateOnly>(type: "date", nullable: true),
                    serviceAuthEnd = table.Column<DateOnly>(type: "date", nullable: true),
                    authorization_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    service_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    units_approved = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    numberVisitsRemaining = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    perWM = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    minutes_per_session = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    units_authorized = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    makeupVisitAllowed = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    numberMakeUps = table.Column<int>(type: "int", nullable: true),
                    makeUpsRemaining = table.Column<int>(type: "int", nullable: true),
                    coVisitAllowed = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    covisitsRemaining = table.Column<int>(type: "int", nullable: true),
                    numberCoVisits = table.Column<int>(type: "int", nullable: true),
                    provider_npi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dxCode2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dxCode3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dxCode4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dxCode5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "session_note_tbl",
                columns: table => new
                {
                    sessionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    session_date = table.Column<DateOnly>(type: "date", nullable: true),
                    session_cpt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    session_cpt1_units = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    session_cpt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    session_cpt2_units = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    session_cpt3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    session_cpt3_units = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    session_cpt4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    session_cpt4_units = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    session_section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EI_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    service_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    service_location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    date_note_written = table.Column<DateOnly>(type: "date", nullable: true),
                    time_from = table.Column<TimeOnly>(type: "time", nullable: true),
                    time_to = table.Column<TimeOnly>(type: "time", nullable: true),
                    ICD10_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    session_cancelled = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    session_madeup_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    make_up_session = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    make_up_for = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    participants = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    participant_other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    participant_unavailble = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    describe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    additional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    outcomes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    routine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strategies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    how_work = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    section_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    therapist_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    therapist_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    relationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    parent_time_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    therapist_time_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_gps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Therapist_gps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoVisit_present = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Draft_Final = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service_authorization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Attendance_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Routine_other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strategies_other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    How_work_other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship_other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionNote_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    service_log_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    service_log_gps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    service_log_timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeSpent = table.Column<TimeOnly>(type: "time", nullable: true),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "user_info_tbl",
                columns: table => new
                {
                    provider_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    middle_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Credentials = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    license_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    npi_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "service_auth_tbl");

            migrationBuilder.DropTable(
                name: "session_note_tbl");

            migrationBuilder.DropTable(
                name: "user_info_tbl");
        }
    }
}
