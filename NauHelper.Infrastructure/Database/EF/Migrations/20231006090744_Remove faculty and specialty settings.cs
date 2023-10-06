using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NauHelper.Infrastructure.Database.EF.Migrations
{
    /// <inheritdoc />
    public partial class Removefacultyandspecialtysettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleSettingKeys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoleSettingKeys",
                keyColumn: "Id",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleSettingKeys",
                columns: new[] { "Id", "DefaultValue", "Key", "RoleId", "Type" },
                values: new object[,]
                {
                    { 2, "", "Faculty", 1, "System.Int32" },
                    { 4, "", "Specialty", 1, "System.Int32" }
                });
        }
    }
}
