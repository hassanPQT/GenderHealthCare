using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReStructureDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Consultant",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_User",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_User",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_Service_ServiceId",
                table: "MedicalHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MenstrualCycle_User",
                table: "MenstrualCycle");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_User",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffSchedule_Consultant",
                table: "StaffSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TestBooking_Service",
                table: "TestBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TestBooking_User",
                table: "TestBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResult_Service",
                table: "TestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResult_TestBooking",
                table: "TestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResult_User",
                table: "TestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_TestResult_ServiceId",
                table: "TestResult");

            migrationBuilder.DropIndex(
                name: "IX_TestResult_TestBookingId",
                table: "TestResult");

            migrationBuilder.DropIndex(
                name: "IX_TestResult_UserId",
                table: "TestResult");

            migrationBuilder.DropIndex(
                name: "IX_TestBooking_ServiceId",
                table: "TestBooking");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistory_ServiceId",
                table: "MedicalHistory");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistory_UserId",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "TestBookingId",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "Tittle",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "MedicalHistory");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "TestBooking",
                newName: "BookingStaffId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StaffSchedule",
                newName: "ConsultantId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffSchedule_UserId",
                table: "StaffSchedule",
                newName: "IX_StaffSchedule_ConsultantId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "User",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "User",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<string>(
                name: "Birthday",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResultDetail",
                table: "TestResult",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "TestResultId",
                table: "TestResult",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TestResult",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TestBookingServiceId",
                table: "TestResult",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TestName",
                table: "TestResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TestResult",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TestBooking",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "TestBooking",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "TestBookingId",
                table: "TestBooking",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TestBooking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TestBooking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "StaffSchedule",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "StaffScheduleId",
                table: "StaffSchedule",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffSchedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StaffSchedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Service",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Service",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Service",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Service",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Role",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10,
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Question",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Question",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Question",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Question",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Question",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "MenstrualCycle",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "MenstrualCycleId",
                table: "MenstrualCycle",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MenstrualCycle",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MenstrualCycle",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "MedicalHistory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Feedback",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Feedback",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Feedback",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackId",
                table: "Feedback",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<string>(
                name: "Tittle",
                table: "Blog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Blog",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogId",
                table: "Blog",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Blog",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "MeetingUrl",
                table: "Appointment",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConsultantId",
                table: "Appointment",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "Appointment",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10,
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Appointment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointment",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Appointment",
                type: "datetime2",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("2bd04214-d426-49c1-b92c-061ca1057aa2"),
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { null, 45m, null });

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("92156da3-b20c-4b53-b0e4-748adaea4a75"),
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { null, 40m, null });

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("c87031b9-f5ea-4494-a2f1-65743f194b8d"),
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { null, 40m, null });

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("d220feba-eb1e-47d6-bc88-a044dcd45025"),
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { null, 50m, null });

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_TestBookingServiceId",
                table: "TestResult",
                column: "TestBookingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_UserId",
                table: "MedicalHistory",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestBookingService_ServiceId",
                table: "TestBookingService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBookingService_TestBookingId",
                table: "TestBookingService",
                column: "TestBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Consultant",
                table: "Appointment",
                column: "ConsultantId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_User",
                table: "Appointment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User",
                table: "Blog",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_User",
                table: "Feedback",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenstrualCycle_User",
                table: "MenstrualCycle",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_User",
                table: "Question",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSchedule_Consultant",
                table: "StaffSchedule",
                column: "ConsultantId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestBooking_User",
                table: "TestBooking",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResult_TestBookingService",
                table: "TestResult",
                column: "TestBookingServiceId",
                principalTable: "TestBookingService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Consultant",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_User",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_User",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_MenstrualCycle_User",
                table: "MenstrualCycle");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_User",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffSchedule_Consultant",
                table: "StaffSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TestBooking_User",
                table: "TestBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResult_TestBookingService",
                table: "TestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "User");

            migrationBuilder.DropTable(
                name: "TestBookingService");

            migrationBuilder.DropIndex(
                name: "IX_User_PhoneNumber",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_TestResult_TestBookingServiceId",
                table: "TestResult");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistory_UserId",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "TestBookingServiceId",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "TestName",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TestBooking");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TestBooking");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StaffSchedule");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StaffSchedule");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MenstrualCycle");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MenstrualCycle");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "BookingStaffId",
                table: "TestBooking",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "ConsultantId",
                table: "StaffSchedule",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffSchedule_ConsultantId",
                table: "StaffSchedule",
                newName: "IX_StaffSchedule_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Dob",
                table: "User",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ResultDetail",
                table: "TestResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TestResultId",
                table: "TestResult",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "TestResult",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TestBookingId",
                table: "TestResult",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TestResult",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TestBooking",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "TestBooking",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<Guid>(
                name: "TestBookingId",
                table: "TestBooking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "StaffSchedule",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "StaffScheduleId",
                table: "StaffSchedule",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Service",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Service",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Service",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Role",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Question",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Question",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Question",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Question",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "Tittle",
                table: "Question",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "MenstrualCycle",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<Guid>(
                name: "MenstrualCycleId",
                table: "MenstrualCycle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "MedicalHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Feedback",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Feedback",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Feedback",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackId",
                table: "Feedback",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Tittle",
                table: "Blog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogId",
                table: "Blog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "MeetingUrl",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<Guid>(
                name: "ConsultantId",
                table: "Appointment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "Appointment",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("2bd04214-d426-49c1-b92c-061ca1057aa2"),
                column: "Price",
                value: 45.0);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("92156da3-b20c-4b53-b0e4-748adaea4a75"),
                column: "Price",
                value: 40.0);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("c87031b9-f5ea-4494-a2f1-65743f194b8d"),
                column: "Price",
                value: 40.0);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("d220feba-eb1e-47d6-bc88-a044dcd45025"),
                column: "Price",
                value: 50.0);

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_ServiceId",
                table: "TestResult",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_TestBookingId",
                table: "TestResult",
                column: "TestBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_UserId",
                table: "TestResult",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_ServiceId",
                table: "TestBooking",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_ServiceId",
                table: "MedicalHistory",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_UserId",
                table: "MedicalHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Consultant",
                table: "Appointment",
                column: "ConsultantId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_User",
                table: "Appointment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User",
                table: "Blog",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_User",
                table: "Feedback",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_Service_ServiceId",
                table: "MedicalHistory",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenstrualCycle_User",
                table: "MenstrualCycle",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_User",
                table: "Question",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSchedule_Consultant",
                table: "StaffSchedule",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestBooking_Service",
                table: "TestBooking",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestBooking_User",
                table: "TestBooking",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResult_Service",
                table: "TestResult",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResult_TestBooking",
                table: "TestResult",
                column: "TestBookingId",
                principalTable: "TestBooking",
                principalColumn: "TestBookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResult_User",
                table: "TestResult",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId");
        }
    }
}
