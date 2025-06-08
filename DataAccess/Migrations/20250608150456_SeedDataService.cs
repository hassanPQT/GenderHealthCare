using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ServiceId", "Description", "IsActive", "Price", "ServiceName" },
                values: new object[,]
                {
                    { new Guid("2bd04214-d426-49c1-b92c-061ca1057aa2"), "Blood test to detect Syphilis infection.", true, 45.0, "Syphilis Test" },
                    { new Guid("92156da3-b20c-4b53-b0e4-748adaea4a75"), "Urine or swab test to detect Chlamydia infection.", true, 40.0, "Chlamydia Test" },
                    { new Guid("c87031b9-f5ea-4494-a2f1-65743f194b8d"), "Swab or urine test to diagnose Gonorrhea.", true, 40.0, "Gonorrhea Test" },
                    { new Guid("d220feba-eb1e-47d6-bc88-a044dcd45025"), "Blood test to detect HIV antibodies or antigens.", true, 50.0, "HIV Test" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("2bd04214-d426-49c1-b92c-061ca1057aa2"));

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("92156da3-b20c-4b53-b0e4-748adaea4a75"));

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("c87031b9-f5ea-4494-a2f1-65743f194b8d"));

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: new Guid("d220feba-eb1e-47d6-bc88-a044dcd45025"));
        }
    }
}
