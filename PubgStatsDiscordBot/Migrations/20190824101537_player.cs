using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PubgStatsDiscordBot.Migrations
{
    public partial class player : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    BestRankPoint = table.Column<float>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    DailyKills = table.Column<int>(nullable: false),
                    DailyWins = table.Column<int>(nullable: false),
                    DamageDealt = table.Column<float>(nullable: false),
                    HeadshotKills = table.Column<int>(nullable: false),
                    Kills = table.Column<int>(nullable: false),
                    LongestKill = table.Column<float>(nullable: false),
                    MaxKillStreaks = table.Column<int>(nullable: false),
                    RankPoints = table.Column<float>(nullable: false),
                    RoundsPlayed = table.Column<int>(nullable: false),
                    Top10s = table.Column<int>(nullable: false),
                    WeeklyKills = table.Column<int>(nullable: false),
                    WeeklyWins = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiscordName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SoloStatsID = table.Column<string>(nullable: true),
                    DuoStatsID = table.Column<string>(nullable: true),
                    SquadStatsID = table.Column<string>(nullable: true),
                    CurrentSeasionLastUpdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Stats_DuoStatsID",
                        column: x => x.DuoStatsID,
                        principalTable: "Stats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Stats_SoloStatsID",
                        column: x => x.SoloStatsID,
                        principalTable: "Stats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Stats_SquadStatsID",
                        column: x => x.SquadStatsID,
                        principalTable: "Stats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_DuoStatsID",
                table: "Players",
                column: "DuoStatsID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_SoloStatsID",
                table: "Players",
                column: "SoloStatsID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_SquadStatsID",
                table: "Players",
                column: "SquadStatsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Stats");
        }
    }
}
