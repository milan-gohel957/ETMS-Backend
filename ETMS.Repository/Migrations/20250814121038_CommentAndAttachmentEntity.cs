using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CommentAndAttachmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tasks_TaskId",
                table: "Attachments");

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

            migrationBuilder.DropCheckConstraint(
                name: "CK_Attachment_OneParent",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CK_Comment_OneParent",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropIndex(
                name: "IX_CK_Comment_OneParent_TaskId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent");

            migrationBuilder.RenameTable(
                name: "CK_Comment_OneParent",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Attachments",
                newName: "ProjectTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_TaskId",
                table: "Attachments",
                newName: "IX_Attachments_ProjectTaskId");

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
                name: "IX_CK_Comment_OneParent_ProjectId",
                table: "Comments",
                newName: "IX_Comments_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_CK_Comment_OneParent_CreatedByUserId",
                table: "Comments",
                newName: "IX_Comments_CreatedByUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(5032));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(5142));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(5164));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(5182));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(5218));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(4872));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 14, 12, 10, 37, 503, DateTimeKind.Utc).AddTicks(4876));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProjectTaskId",
                table: "Comments",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tasks_ProjectTaskId",
                table: "Attachments",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Project_ProjectId",
                table: "Comments",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_ProjectTaskId",
                table: "Comments",
                column: "ProjectTaskId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tasks_ProjectTaskId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Project_ProjectId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_ProjectTaskId",
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

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProjectTaskId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "Comments");

            migrationBuilder.EnsureSchema(
                name: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "CK_Comment_OneParent",
                newSchema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)");

            migrationBuilder.RenameColumn(
                name: "ProjectTaskId",
                table: "Attachments",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_ProjectTaskId",
                table: "Attachments",
                newName: "IX_Attachments_TaskId");

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
                name: "IX_Comments_ProjectId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CreatedByUserId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                newName: "IX_CK_Comment_OneParent_CreatedByUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Attachments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddCheckConstraint(
                name: "CK_Attachment_OneParent",
                table: "Attachments",
                sql: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_CK_Comment_OneParent_TaskId",
                schema: "([ProjectId] IS NOT NULL AND [TaskId] IS NULL) OR ([ProjectId] IS NULL AND [TaskId] IS NOT NULL)",
                table: "CK_Comment_OneParent",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tasks_TaskId",
                table: "Attachments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
