using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<bool>(type: "bit", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Birthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PublistDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blog_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_Service",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "ServiceId");
                    table.ForeignKey(
                        name: "FK_Feedback_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    MedicalHistoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.MedicalHistoryId);
                    table.ForeignKey(
                        name: "FK_MedicalHistory_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenstrualCycle",
                columns: table => new
                {
                    MenstrualCycleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvulationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FertilityWindowStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FertilityWindowEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PillReminder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenstrualCycle", x => x.MenstrualCycleId);
                    table.ForeignKey(
                        name: "FK_MenstrualCycle_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AnswerDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Consultant",
                        column: x => x.ConsultantId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Question_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffSchedule",
                columns: table => new
                {
                    StaffScheduleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    WorkingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffSchedule", x => x.StaffScheduleId);
                    table.ForeignKey(
                        name: "FK_StaffSchedule_Consultant",
                        column: x => x.ConsultantId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestBooking",
                columns: table => new
                {
                    TestBookingId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    MedicalHistoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookingStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestBooking", x => x.TestBookingId);
                    table.ForeignKey(
                        name: "FK_TestBooking_MedicalHistory",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistory",
                        principalColumn: "MedicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestBooking_Staff",
                        column: x => x.StaffId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_TestBooking_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    StaffScheduleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointment_Consultant",
                        column: x => x.ConsultantId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_StaffSchedule",
                        column: x => x.StaffScheduleId,
                        principalTable: "StaffSchedule",
                        principalColumn: "StaffScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestBookingService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    TestBookingId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestBookingService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestBookingService_Service",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestBookingService_TestBooking",
                        column: x => x.TestBookingId,
                        principalTable: "TestBooking",
                        principalColumn: "TestBookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResult",
                columns: table => new
                {
                    TestResultId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    TestBookingServiceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 10, nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResultDetail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SampleReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResult", x => x.TestResultId);
                    table.ForeignKey(
                        name: "FK_TestResult_TestBookingService",
                        column: x => x.TestBookingServiceId,
                        principalTable: "TestBookingService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("157f0b62-afbb-44ce-91ce-397239875df5"), "Consultant" },
                    { new Guid("c5b82656-c6a7-49bd-a3fb-3d3e07022d33"), "Manager" },
                    { new Guid("cb923e1c-ed85-45a8-bc2f-8b78c60b7e28"), "Customer" },
                    { new Guid("d5cf10f1-1f31-4016-ac13-34667e9ca10d"), "Staff" }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ServiceId", "CreatedAt", "Description", "IsActive", "Price", "ServiceName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2bd04214-d426-49c1-b92c-061ca1057aa2"), null, "Blood test to detect Syphilis infection.", true, 45m, "Syphilis Test", null },
                    { new Guid("92156da3-b20c-4b53-b0e4-748adaea4a75"), null, "Urine or swab test to detect Chlamydia infection.", true, 40m, "Chlamydia Test", null },
                    { new Guid("c87031b9-f5ea-4494-a2f1-65743f194b8d"), null, "Swab or urine test to diagnose Gonorrhea.", true, 40m, "Gonorrhea Test", null },
                    { new Guid("d220feba-eb1e-47d6-bc88-a044dcd45025"), null, "Blood test to detect HIV antibodies or antigens.", true, 50m, "HIV Test", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ConsultantId",
                table: "Appointment",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_StaffScheduleId",
                table: "Appointment",
                column: "StaffScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_UserId",
                table: "Blog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ServiceId",
                table: "Feedback",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserId",
                table: "Feedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_UserId",
                table: "MedicalHistory",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenstrualCycle_UserId",
                table: "MenstrualCycle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ConsultantId",
                table: "Question",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_UserId",
                table: "Question",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSchedule_ConsultantId",
                table: "StaffSchedule",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_MedicalHistoryId",
                table: "TestBooking",
                column: "MedicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_StaffId",
                table: "TestBooking",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_UserId",
                table: "TestBooking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBookingService_ServiceId",
                table: "TestBookingService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBookingService_TestBookingId",
                table: "TestBookingService",
                column: "TestBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_TestBookingServiceId",
                table: "TestResult",
                column: "TestBookingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "MenstrualCycle");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "TestResult");

            migrationBuilder.DropTable(
                name: "StaffSchedule");

            migrationBuilder.DropTable(
                name: "TestBookingService");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "TestBooking");

            migrationBuilder.DropTable(
                name: "MedicalHistory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
