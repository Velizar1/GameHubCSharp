using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHubCSharp.Migrations
{
    public partial class ststusofplayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_GameEvents_GameEventId",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "GameEventId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_GameEvents_GameEventId",
                table: "Notifications",
                column: "GameEventId",
                principalTable: "GameEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_GameEvents_GameEventId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameEventId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_GameEvents_GameEventId",
                table: "Notifications",
                column: "GameEventId",
                principalTable: "GameEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
