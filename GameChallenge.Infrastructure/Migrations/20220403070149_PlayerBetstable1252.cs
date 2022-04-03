using Microsoft.EntityFrameworkCore.Migrations;

namespace GameChallenge.Infrastructure.Migrations
{
    public partial class PlayerBetstable1252 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Description", "Name", "Value" },
                values: new object[] { 1, "Random minimum number in a challenge", "RandomNumberMin", "0" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Description", "Name", "Value" },
                values: new object[] { 2, "Random maximum number in a challenge", "RandomNumberMax", "9" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Description", "Name", "Value" },
                values: new object[] { 3, "e.g. If he is right, he gets 9 times his stake as a prize", "RewardHowManyTimes", "9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
