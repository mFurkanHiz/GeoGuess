using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoGuess.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class panoramalabeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Panoramas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "Panoramas");
        }
    }
}
