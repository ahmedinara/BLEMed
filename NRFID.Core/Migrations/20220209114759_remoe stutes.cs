using Microsoft.EntityFrameworkCore.Migrations;

namespace NFC.Core.Migrations
{
    public partial class remoestutes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "ApplicationGroupSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "ApplicationGroupSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
