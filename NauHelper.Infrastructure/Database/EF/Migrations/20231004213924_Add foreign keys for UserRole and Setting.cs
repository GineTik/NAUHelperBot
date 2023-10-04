using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NauHelper.Infrastructure.Database.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddforeignkeysforUserRoleandSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_RoleSettingKeyId",
                table: "Settings",
                column: "RoleSettingKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserId",
                table: "Settings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_RoleSettingKeys_RoleSettingKeyId",
                table: "Settings",
                column: "RoleSettingKeyId",
                principalTable: "RoleSettingKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Users_UserId",
                table: "Settings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "TelegramId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "TelegramId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_RoleSettingKeys_RoleSettingKeyId",
                table: "Settings");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Users_UserId",
                table: "Settings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Settings_RoleSettingKeyId",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_UserId",
                table: "Settings");
        }
    }
}
