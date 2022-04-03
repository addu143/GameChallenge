using Microsoft.EntityFrameworkCore.Migrations;

namespace GameChallenge.Infrastructure.Migrations
{
    public partial class PlayerBetstable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountWin",
                table: "PlayerBets",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "PlayerBets",
                newName: "AmountWin");
        }
    }
}
