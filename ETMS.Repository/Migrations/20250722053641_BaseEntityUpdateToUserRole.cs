using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntityUpdateToUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2025, 7, 22, 5, 36, 40, 421, DateTimeKind.Utc).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 36, 40, 421, DateTimeKind.Utc).AddTicks(9227));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 36, 40, 421, DateTimeKind.Utc).AddTicks(9259));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 36, 40, 421, DateTimeKind.Utc).AddTicks(9279));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 36, 40, 421, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 36, 40, 421, DateTimeKind.Utc).AddTicks(9321));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2025, 7, 22, 5, 33, 5, 359, DateTimeKind.Utc).AddTicks(8949));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 33, 5, 359, DateTimeKind.Utc).AddTicks(9069));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 33, 5, 359, DateTimeKind.Utc).AddTicks(9096));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 33, 5, 359, DateTimeKind.Utc).AddTicks(9119));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 33, 5, 359, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 22, 5, 33, 5, 359, DateTimeKind.Utc).AddTicks(9163));
        }
    }
}
