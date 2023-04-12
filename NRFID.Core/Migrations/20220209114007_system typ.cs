using Microsoft.EntityFrameworkCore.Migrations;

namespace NFC.Core.Migrations
{
    public partial class systemtyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNo",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "ApplicationGroupSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "ApplicationGroupSettings");

            migrationBuilder.AddColumn<string>(
                name: "CardNo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
