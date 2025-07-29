using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTask2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Tasks_ProjectTaskId",
                table: "UserTask");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(938));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(1093));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(1174));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 12, 3, 18, 250, DateTimeKind.Utc).AddTicks(675));

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Tasks_ProjectTaskId",
                table: "UserTask",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Tasks_ProjectTaskId",
                table: "UserTask");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(4959));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(5085));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(5137));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(5159));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(5179));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 29, 11, 50, 42, 636, DateTimeKind.Utc).AddTicks(4631));

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Tasks_ProjectTaskId",
                table: "UserTask",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
