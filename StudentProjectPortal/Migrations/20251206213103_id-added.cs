using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjectPortal.Migrations
{
    /// <inheritdoc />
    public partial class idadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuperVisors",
                table: "SuperVisors");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "SuperVisors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SuperVisors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuperVisors",
                table: "SuperVisors",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuperVisors",
                table: "SuperVisors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SuperVisors");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "SuperVisors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuperVisors",
                table: "SuperVisors",
                column: "StaffId");
        }
    }
}
