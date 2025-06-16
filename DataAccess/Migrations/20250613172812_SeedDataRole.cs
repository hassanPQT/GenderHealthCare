using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("157f0b62-afbb-44ce-91ce-397239875df5"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("c5b82656-c6a7-49bd-a3fb-3d3e07022d33"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("cb923e1c-ed85-45a8-bc2f-8b78c60b7e28"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("d5cf10f1-1f31-4016-ac13-34667e9ca10d"));
        }
    }
}
