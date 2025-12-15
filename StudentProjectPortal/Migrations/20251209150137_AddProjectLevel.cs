using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjectPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Classes_ClassId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ClassId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Projects",
                newName: "Level");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Projects",
                newName: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClassId",
                table: "Projects",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Classes_ClassId",
                table: "Projects",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
