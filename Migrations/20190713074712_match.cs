using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballOdds.Migrations
{
    public partial class match : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hometeam = table.Column<int>(nullable: true),
                    Awayteam = table.Column<int>(nullable: true),
                    Homegoals = table.Column<int>(nullable: false),
                    Awaygoals = table.Column<int>(nullable: false),
                    Result = table.Column<string>(maxLength: 5, nullable: false),
                    MatchDay = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Team_Awayteam",
                        column: x => x.Awayteam,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_Hometeam",
                        column: x => x.Hometeam,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_Awayteam",
                table: "Match",
                column: "Awayteam");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Hometeam",
                table: "Match",
                column: "Hometeam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
