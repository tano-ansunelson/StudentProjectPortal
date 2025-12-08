using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjectPortal.Migrations
{
    /// <inheritdoc />
    public partial class typeoremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuperVisoor",
                table: "SuperVisors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SuperVisoor",
                table: "SuperVisors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
