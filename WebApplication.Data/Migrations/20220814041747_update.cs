using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Person_OwnerId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_OwnerId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Car");

            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Car",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Car_OwnerId",
                table: "Car",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Person_OwnerId",
                table: "Car",
                column: "OwnerId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
