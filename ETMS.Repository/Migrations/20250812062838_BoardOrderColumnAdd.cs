using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class BoardOrderColumnAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Project_ProjectId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_CreatedByUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UpdatedByUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId1",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Comment_OneParent",
                table: "Comments");

            migrationBuilder.EnsureSchema(
                name: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "CK_Comment_OneParent",
                newSchema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId1",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UpdatedByUserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TaskId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProjectId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CreatedByUserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_CreatedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Boards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CK_Comment_OneParent",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4224));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4393));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4423));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4446));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4469));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4491));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4513));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4028));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 12, 6, 28, 36, 54, DateTimeKind.Utc).AddTicks(4031));

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Comment_OneParent_Project_ProjectId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Comment_OneParent_Tasks_TaskId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Comment_OneParent_Users_CreatedByUserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Comment_OneParent_Users_UpdatedByUserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Comment_OneParent_Users_UserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Comment_OneParent_Users_UserId1",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CK_Comment_OneParent_Project_ProjectId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropForeignKey(
                name: "FK_CK_Comment_OneParent_Tasks_TaskId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropForeignKey(
                name: "FK_CK_Comment_OneParent_Users_CreatedByUserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropForeignKey(
                name: "FK_CK_Comment_OneParent_Users_UpdatedByUserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropForeignKey(
                name: "FK_CK_Comment_OneParent_Users_UserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropForeignKey(
                name: "FK_CK_Comment_OneParent_Users_UserId1",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CK_Comment_OneParent",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "CK_Comment_OneParent",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_CK_Comment_OneParent_UserId1",
                table: "Comments",
                newName: "IX_Comments_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_CK_Comment_OneParent_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CK_Comment_OneParent_UpdatedByUserId",
                table: "Comments",
                newName: "IX_Comments_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CK_Comment_OneParent_TaskId",
                table: "Comments",
                newName: "IX_Comments_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_CK_Comment_OneParent_ProjectId",
                table: "Comments",
                newName: "IX_Comments_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_CK_Comment_OneParent_CreatedByUserId",
                table: "Comments",
                newName: "IX_Comments_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5563));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5681));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5706));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5727));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5745));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5764));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5794));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 8, 42, 39, 408, DateTimeKind.Utc).AddTicks(5388));

            migrationBuilder.AddCheckConstraint(
                name: "CK_Comment_OneParent",
                table: "Comments",
                sql: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Project_ProjectId",
                table: "Comments",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_CreatedByUserId",
                table: "Comments",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UpdatedByUserId",
                table: "Comments",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId1",
                table: "Comments",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
