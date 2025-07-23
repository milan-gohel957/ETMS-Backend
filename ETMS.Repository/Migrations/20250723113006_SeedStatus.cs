using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 11, 30, 4, 981, DateTimeKind.Utc).AddTicks(9951));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 11, 30, 4, 982, DateTimeKind.Utc).AddTicks(62));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 11, 30, 4, 982, DateTimeKind.Utc).AddTicks(87));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 11, 30, 4, 982, DateTimeKind.Utc).AddTicks(107));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 11, 30, 4, 982, DateTimeKind.Utc).AddTicks(126));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 11, 30, 4, 982, DateTimeKind.Utc).AddTicks(145));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 11, 30, 4, 982, DateTimeKind.Utc).AddTicks(164));

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "ColorCode", "CreatedAt", "CreatedByUserId", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedByUserId" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2025, 7, 23, 11, 30, 4, 981, DateTimeKind.Utc).AddTicks(9757), null, null, false, "Pending", null, null },
                    { 2, "", new DateTime(2025, 7, 23, 11, 30, 4, 981, DateTimeKind.Utc).AddTicks(9760), null, null, false, "Completed", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 7, 29, 10, 463, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 7, 29, 10, 463, DateTimeKind.Utc).AddTicks(7761));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 7, 29, 10, 463, DateTimeKind.Utc).AddTicks(7795));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 7, 29, 10, 463, DateTimeKind.Utc).AddTicks(7821));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 7, 29, 10, 463, DateTimeKind.Utc).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 7, 29, 10, 463, DateTimeKind.Utc).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 7, 29, 10, 463, DateTimeKind.Utc).AddTicks(7913));
        }
    }
}
