using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NauHelper.Infrastructure.Database.EF.Migrations
{
    /// <inheritdoc />
    public partial class Addforeignkeysforspecialtyandgroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Specialties_FacultyId",
                table: "Specialties",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecialtyId",
                table: "Groups",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialties_SpecialtyId",
                table: "Groups",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_Faculties_FacultyId",
                table: "Specialties",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialties_SpecialtyId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_Faculties_FacultyId",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_FacultyId",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Groups_SpecialtyId",
                table: "Groups");
        }
    }
}
