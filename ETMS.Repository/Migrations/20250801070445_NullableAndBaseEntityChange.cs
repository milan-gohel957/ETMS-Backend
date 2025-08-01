using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMS.Repository.Migrations
{
    /// <inheritdoc />
    public partial class NullableAndBaseEntityChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MileStones_Users_CreatedByUserId",
                table: "MileStones");

            migrationBuilder.DropForeignKey(
                name: "FK_MileStones_Users_UpdatedByUserId",
                table: "MileStones");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_CreatedByUserId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_UpdatedByUserId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Users_CreatedByUserId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Users_UpdatedByUserId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_CreatedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Users_CreatedByUserId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Users_UpdatedByUserId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_CreatedByUserId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_UpdatedByUserId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Roles_CreatedByUserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_CreatedByUserId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_UpdatedByUserId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_CreatedByUserId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_UpdatedByUserId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_MileStones_CreatedByUserId",
                table: "MileStones");

            migrationBuilder.DropIndex(
                name: "IX_MileStones_UpdatedByUserId",
                table: "MileStones");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "MileStones");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "MileStones");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(4074));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(4395));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(4441));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(4491));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(4586));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(3819));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 1, 7, 4, 45, 216, DateTimeKind.Utc).AddTicks(3824));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Statuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Statuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "MileStones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "MileStones",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1377), null, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1593), null, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1631), null, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1657), null, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1682), null, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1705), null, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1727), null, null });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1154), null, null });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedByUserId" },
                values: new object[] { new DateTime(2025, 8, 1, 6, 57, 2, 235, DateTimeKind.Utc).AddTicks(1158), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_CreatedByUserId",
                table: "Statuses",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_UpdatedByUserId",
                table: "Statuses",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedByUserId",
                table: "Roles",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdatedByUserId",
                table: "Roles",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CreatedByUserId",
                table: "RolePermissions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_UpdatedByUserId",
                table: "RolePermissions",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedByUserId",
                table: "Permissions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UpdatedByUserId",
                table: "Permissions",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MileStones_CreatedByUserId",
                table: "MileStones",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MileStones_UpdatedByUserId",
                table: "MileStones",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MileStones_Users_CreatedByUserId",
                table: "MileStones",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MileStones_Users_UpdatedByUserId",
                table: "MileStones",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_CreatedByUserId",
                table: "Permissions",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UpdatedByUserId",
                table: "Permissions",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Users_CreatedByUserId",
                table: "RolePermissions",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Users_UpdatedByUserId",
                table: "RolePermissions",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_CreatedByUserId",
                table: "Roles",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UpdatedByUserId",
                table: "Roles",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Users_CreatedByUserId",
                table: "Statuses",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Users_UpdatedByUserId",
                table: "Statuses",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
