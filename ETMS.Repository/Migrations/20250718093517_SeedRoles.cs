using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByUserId", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy", "UpdatedByUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 18, 9, 35, 15, 739, DateTimeKind.Utc).AddTicks(9900), null, null, "This is Admin Role. It will have all the permissions.", false, "Admin", null, null, null },
                    { 2, new DateTime(2025, 7, 18, 9, 35, 15, 740, DateTimeKind.Utc).AddTicks(134), null, null, "This is Program Manger Role.", false, "Program Manager", null, null, null },
                    { 3, new DateTime(2025, 7, 18, 9, 35, 15, 740, DateTimeKind.Utc).AddTicks(212), null, null, "This is Project Manger Role.", false, "Project Manager", null, null, null },
                    { 4, new DateTime(2025, 7, 18, 9, 35, 15, 740, DateTimeKind.Utc).AddTicks(258), null, null, "This is Team Lead Role.", false, "Team Lead", null, null, null },
                    { 5, new DateTime(2025, 7, 18, 9, 35, 15, 740, DateTimeKind.Utc).AddTicks(308), null, null, "This is Senior Developer Role.", false, "Senior Developer", null, null, null },
                    { 6, new DateTime(2025, 7, 18, 9, 35, 15, 740, DateTimeKind.Utc).AddTicks(351), null, null, "This is Junior Developer Role.", false, "Junior Developer", null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
