using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipMedicalHistoryTestBookingTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_Service",
                table: "MedicalHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResult_MedicalHistory",
                table: "TestResult");

            migrationBuilder.RenameColumn(
                name: "MedicalHistoryId",
                table: "TestResult",
                newName: "TestBookingId");

            migrationBuilder.RenameIndex(
                name: "IX_TestResult_MedicalHistoryId",
                table: "TestResult",
                newName: "IX_TestResult_TestBookingId");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalHistoryId",
                table: "TestBooking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "MedicalHistory",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_MedicalHistoryId",
                table: "TestBooking",
                column: "MedicalHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_Service_ServiceId",
                table: "MedicalHistory",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestBooking_MedicalHistory",
                table: "TestBooking",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistory",
                principalColumn: "MedicalHistoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResult_TestBooking",
                table: "TestResult",
                column: "TestBookingId",
                principalTable: "TestBooking",
                principalColumn: "TestBookingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_Service_ServiceId",
                table: "MedicalHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TestBooking_MedicalHistory",
                table: "TestBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResult_TestBooking",
                table: "TestResult");

            migrationBuilder.DropIndex(
                name: "IX_TestBooking_MedicalHistoryId",
                table: "TestBooking");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId",
                table: "TestBooking");

            migrationBuilder.RenameColumn(
                name: "TestBookingId",
                table: "TestResult",
                newName: "MedicalHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TestResult_TestBookingId",
                table: "TestResult",
                newName: "IX_TestResult_MedicalHistoryId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "MedicalHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_Service",
                table: "MedicalHistory",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResult_MedicalHistory",
                table: "TestResult",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistory",
                principalColumn: "MedicalHistoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
