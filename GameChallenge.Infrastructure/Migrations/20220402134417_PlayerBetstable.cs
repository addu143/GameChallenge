using Microsoft.EntityFrameworkCore.Migrations;

namespace GameChallenge.Infrastructure.Migrations
{
    public partial class PlayerBetstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerBets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RandomNumberByUser = table.Column<int>(type: "INTEGER", nullable: false),
                    RandomNumberBySystem = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountWin = table.Column<double>(type: "REAL", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerBets_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBets_PlayerId",
                table: "PlayerBets",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerBets");
        }
    }
}
