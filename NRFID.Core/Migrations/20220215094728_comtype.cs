using Microsoft.EntityFrameworkCore.Migrations;

namespace NFC.Core.Migrations
{
    public partial class comtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CommRoleDetials");

            migrationBuilder.AddColumn<string>(
                name: "CommType",
                table: "CommRoleDetials",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommType",
                table: "CommRoleDetials");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CommRoleDetials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
