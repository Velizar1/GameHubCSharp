using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHubCSharp.Migrations
{
    public partial class first2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "GameEvents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "GameEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
