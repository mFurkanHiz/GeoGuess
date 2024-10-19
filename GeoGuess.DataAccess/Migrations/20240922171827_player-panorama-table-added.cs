using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoGuess.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class playerpanoramatableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PseudoPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PseudoPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPanoramas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    PanoramaId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPanoramas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerPanoramas_Panoramas_PanoramaId",
                        column: x => x.PanoramaId,
                        principalTable: "Panoramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPanoramas_PseudoPlayers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "PseudoPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPanoramas_PanoramaId",
                table: "PlayerPanoramas",
                column: "PanoramaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPanoramas_PlayerId",
                table: "PlayerPanoramas",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPanoramas");

            migrationBuilder.DropTable(
                name: "PseudoPlayers");
        }
    }
}
