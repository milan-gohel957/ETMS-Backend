using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserRolesFieldsInUserAndRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_CreatedByUserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UpdatedByUserId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_CreatedByUserId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UpdatedByUserId",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "UserRole");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 7, 14, 17, 953, DateTimeKind.Utc).AddTicks(6589));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 7, 14, 17, 953, DateTimeKind.Utc).AddTicks(6693));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 7, 14, 17, 953, DateTimeKind.Utc).AddTicks(6718));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 7, 14, 17, 953, DateTimeKind.Utc).AddTicks(6737));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 7, 14, 17, 953, DateTimeKind.Utc).AddTicks(6756));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 7, 14, 17, 953, DateTimeKind.Utc).AddTicks(6776));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 7, 14, 17, 953, DateTimeKind.Utc).AddTicks(6794));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "UserRole",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "UserRole",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 6, 6, 24, 89, DateTimeKind.Utc).AddTicks(6666));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 6, 6, 24, 89, DateTimeKind.Utc).AddTicks(6804));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 6, 6, 24, 89, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 6, 6, 24, 89, DateTimeKind.Utc).AddTicks(6850));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 6, 6, 24, 89, DateTimeKind.Utc).AddTicks(6871));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 6, 6, 24, 89, DateTimeKind.Utc).AddTicks(6892));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 6, 6, 24, 89, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_CreatedByUserId",
                table: "UserRole",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UpdatedByUserId",
                table: "UserRole",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_CreatedByUserId",
                table: "UserRole",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UpdatedByUserId",
                table: "UserRole",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
