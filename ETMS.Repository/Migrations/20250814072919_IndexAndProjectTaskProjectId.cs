using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class IndexAndProjectTaskProjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProjectRole_ProjectId",
                table: "UserProjectRole");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4435));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4551));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4577));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4598));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4634));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4653));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4265));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 7, 29, 18, 992, DateTimeKind.Utc).AddTicks(4267));

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_UserId_ProjectTaskId",
                table: "UserTask",
                columns: new[] { "UserId", "ProjectTaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectRole_ProjectId_RoleId_UserId",
                table: "UserProjectRole",
                columns: new[] { "ProjectId", "RoleId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Project_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Project_ProjectId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTask_UserId_ProjectTaskId",
                table: "UserTask");

            migrationBuilder.DropIndex(
                name: "IX_UserProjectRole_ProjectId_RoleId_UserId",
                table: "UserProjectRole");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(8812));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(9069));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(9134));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(9175));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(9213));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(9241));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(9269));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(8415));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 11, 24, 48, 102, DateTimeKind.Utc).AddTicks(8418));

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectRole_ProjectId",
                table: "UserProjectRole",
                column: "ProjectId");
        }
    }
}
