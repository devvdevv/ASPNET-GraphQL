using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnEFCore.Data.Migrations
{
    public partial class m2m_payload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleSamurai_Battle_BattlesId",
                table: "BattleSamurai");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleSamurai_Samurai_SamuraisId",
                table: "BattleSamurai");

            migrationBuilder.RenameColumn(
                name: "SamuraisId",
                table: "BattleSamurai",
                newName: "SamuraiId");

            migrationBuilder.RenameColumn(
                name: "BattlesId",
                table: "BattleSamurai",
                newName: "BattleId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleSamurai_SamuraisId",
                table: "BattleSamurai",
                newName: "IX_BattleSamurai_SamuraiId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateJoined",
                table: "BattleSamurai",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleSamurai_Battle_BattleId",
                table: "BattleSamurai",
                column: "BattleId",
                principalTable: "Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleSamurai_Samurai_SamuraiId",
                table: "BattleSamurai",
                column: "SamuraiId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleSamurai_Battle_BattleId",
                table: "BattleSamurai");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleSamurai_Samurai_SamuraiId",
                table: "BattleSamurai");

            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "BattleSamurai");

            migrationBuilder.RenameColumn(
                name: "SamuraiId",
                table: "BattleSamurai",
                newName: "SamuraisId");

            migrationBuilder.RenameColumn(
                name: "BattleId",
                table: "BattleSamurai",
                newName: "BattlesId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleSamurai_SamuraiId",
                table: "BattleSamurai",
                newName: "IX_BattleSamurai_SamuraisId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleSamurai_Battle_BattlesId",
                table: "BattleSamurai",
                column: "BattlesId",
                principalTable: "Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleSamurai_Samurai_SamuraisId",
                table: "BattleSamurai",
                column: "SamuraisId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
