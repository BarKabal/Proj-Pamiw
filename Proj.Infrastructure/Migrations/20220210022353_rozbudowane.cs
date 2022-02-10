using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proj.Infrastructure.Migrations
{
    public partial class rozbudowane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnounceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: true),
                    PostTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyForm_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplyForm_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignPlayer",
                columns: table => new
                {
                    CampaignsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignPlayer", x => new { x.CampaignsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_CampaignPlayer_Campaign_CampaignsId",
                        column: x => x.CampaignsId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignPlayer_Player_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: true),
                    AnnouncementId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Session_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyForm_CampaignId",
                table: "ApplyForm",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyForm_PlayerId",
                table: "ApplyForm",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPlayer_PlayersId",
                table: "CampaignPlayer",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_AnnouncementId",
                table: "Session",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_CampaignId",
                table: "Session",
                column: "CampaignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyForm");

            migrationBuilder.DropTable(
                name: "CampaignPlayer");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Announcement");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Player");

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampainId = table.Column<int>(type: "int", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Sheet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Campaign_CampainId",
                        column: x => x.CampainId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_CampainId",
                table: "Character",
                column: "CampainId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_PlayerId",
                table: "Character",
                column: "PlayerId");
        }
    }
}
