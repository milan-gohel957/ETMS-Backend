using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RolePermissionIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9874));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9897));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9915));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9932));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9950));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9969));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9573));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 5, 10, 47, 452, DateTimeKind.Utc).AddTicks(9576));
        }
    }
}
