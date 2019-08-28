using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PubgStatsDiscordBot.Migrations
{
    public partial class match : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Stats_DuoStatsID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Stats_SoloStatsID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Stats_SquadStatsID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Players_PlayerID",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stats",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_PlayerID",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "GameMode",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "IsCustomMatch",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "MapName",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Stats");

            migrationBuilder.RenameTable(
                name: "Stats",
                newName: "SeasonStats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonStats",
                table: "SeasonStats",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    GameMode = table.Column<string>(nullable: true),
                    TitleId = table.Column<string>(nullable: true),
                    MapName = table.Column<string>(nullable: true),
                    IsCustomMatch = table.Column<bool>(nullable: false),
                    SeasonState = table.Column<string>(nullable: true),
                    ShardId = table.Column<string>(nullable: true),
                    PlayerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rosters",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Won = table.Column<bool>(nullable: false),
                    ShardId = table.Column<string>(nullable: true),
                    MatchId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rosters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rosters_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StatsID = table.Column<string>(nullable: true),
                    Actor = table.Column<string>(nullable: true),
                    ShardId = table.Column<string>(nullable: true),
                    RosterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Rosters_RosterId",
                        column: x => x.RosterId,
                        principalTable: "Rosters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participants_SeasonStats_StatsID",
                        column: x => x.StatsID,
                        principalTable: "SeasonStats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerId",
                table: "Matches",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_RosterId",
                table: "Participants",
                column: "RosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_StatsID",
                table: "Participants",
                column: "StatsID");

            migrationBuilder.CreateIndex(
                name: "IX_Rosters_MatchId",
                table: "Rosters",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_SeasonStats_DuoStatsID",
                table: "Players",
                column: "DuoStatsID",
                principalTable: "SeasonStats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_SeasonStats_SoloStatsID",
                table: "Players",
                column: "SoloStatsID",
                principalTable: "SeasonStats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_SeasonStats_SquadStatsID",
                table: "Players",
                column: "SquadStatsID",
                principalTable: "SeasonStats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_SeasonStats_DuoStatsID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_SeasonStats_SoloStatsID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_SeasonStats_SquadStatsID",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Rosters");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonStats",
                table: "SeasonStats");

            migrationBuilder.RenameTable(
                name: "SeasonStats",
                newName: "Stats");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameMode",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomMatch",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MapName",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatchId",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerID",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stats",
                table: "Stats",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerID",
                table: "Stats",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Stats_DuoStatsID",
                table: "Players",
                column: "DuoStatsID",
                principalTable: "Stats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Stats_SoloStatsID",
                table: "Players",
                column: "SoloStatsID",
                principalTable: "Stats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Stats_SquadStatsID",
                table: "Players",
                column: "SquadStatsID",
                principalTable: "Stats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Players_PlayerID",
                table: "Stats",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
