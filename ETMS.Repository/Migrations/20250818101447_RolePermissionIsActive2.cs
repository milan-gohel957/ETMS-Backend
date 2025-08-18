using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RolePermissionIsActive2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(8022));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(8080));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(8122));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(8143));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(7533));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 14, 47, 210, DateTimeKind.Utc).AddTicks(7538));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8433));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8545));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8571));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8592));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8610));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8630));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8649));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8261));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 10, 2, 51, 391, DateTimeKind.Utc).AddTicks(8268));
        }
    }
}
