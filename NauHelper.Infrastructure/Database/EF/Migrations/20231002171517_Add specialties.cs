using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NauHelper.Infrastructure.Database.EF.Migrations
{
    /// <inheritdoc />
    public partial class Addspecialties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Groups",
                newName: "SpecialtyId");

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RoleSettingKeys",
                columns: new[] { "Id", "DefaultValue", "Key", "RoleId", "Type" },
                values: new object[] { 4, "", "Specialty", 1, "System.Int32" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DeleteData(
                table: "RoleSettingKeys",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "SpecialtyId",
                table: "Groups",
                newName: "FacultyId");
        }
    }
}
