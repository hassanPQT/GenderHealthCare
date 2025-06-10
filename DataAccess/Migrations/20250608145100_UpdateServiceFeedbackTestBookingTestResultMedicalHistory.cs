using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceFeedbackTestBookingTestResultMedicalHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Feedback",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_TestBooking",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_TestResult",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_FeedbackId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TestBookingId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TestResultId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "BookingStaff",
                table: "TestBooking");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TestBookingId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TestResultId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TestResultId",
                table: "MedicalHistory");

            migrationBuilder.RenameColumn(
                name: "SmapleReceivedDate",
                table: "TestResult",
                newName: "SampleReceivedDate");

            migrationBuilder.RenameColumn(
                name: "MedicallHistoryId",
                table: "MedicalHistory",
                newName: "MedicalHistoryId");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Feedback",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Feedback",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PublistDate",
                table: "Feedback",
                newName: "PublishDate");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "TestResult",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ServiceName",
                table: "Service",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_ServiceId",
                table: "TestResult",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_ServiceId",
                table: "TestBooking",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ServiceId",
                table: "Feedback",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Service",
                table: "Feedback",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestBooking_Service",
                table: "TestBooking",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResult_Service",
                table: "TestResult",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Service",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_TestBooking_Service",
                table: "TestBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResult_Service",
                table: "TestResult");

            migrationBuilder.DropIndex(
                name: "IX_TestResult_ServiceId",
                table: "TestResult");

            migrationBuilder.DropIndex(
                name: "IX_TestBooking_ServiceId",
                table: "TestBooking");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_ServiceId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "TestResult");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "SampleReceivedDate",
                table: "TestResult",
                newName: "SmapleReceivedDate");

            migrationBuilder.RenameColumn(
                name: "MedicalHistoryId",
                table: "MedicalHistory",
                newName: "MedicallHistoryId");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Feedback",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Feedback",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Feedback",
                newName: "PublistDate");

            migrationBuilder.AddColumn<string>(
                name: "BookingStaff",
                table: "TestBooking",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceName",
                table: "Service",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackId",
                table: "Service",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TestBookingId",
                table: "Service",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TestResultId",
                table: "Service",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TestResultId",
                table: "MedicalHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_FeedbackId",
                table: "Service",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TestBookingId",
                table: "Service",
                column: "TestBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TestResultId",
                table: "Service",
                column: "TestResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Feedback",
                table: "Service",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_TestBooking",
                table: "Service",
                column: "TestBookingId",
                principalTable: "TestBooking",
                principalColumn: "TestBookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_TestResult",
                table: "Service",
                column: "TestResultId",
                principalTable: "TestResult",
                principalColumn: "TestResultId");
        }
    }
}
