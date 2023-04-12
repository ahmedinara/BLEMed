using Microsoft.EntityFrameworkCore.Migrations;

namespace NFC.Core.Migrations
{
    public partial class coomerolerelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommType",
                table: "CommRoleDetials");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "CommRoleDetials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CommRoleDetials_TypeId",
                table: "CommRoleDetials",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommRoleDetials_ApplicationSettings_TypeId",
                table: "CommRoleDetials",
                column: "TypeId",
                principalTable: "ApplicationSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommRoleDetials_ApplicationSettings_TypeId",
                table: "CommRoleDetials");

            migrationBuilder.DropIndex(
                name: "IX_CommRoleDetials_TypeId",
                table: "CommRoleDetials");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "CommRoleDetials");

            migrationBuilder.AddColumn<string>(
                name: "CommType",
                table: "CommRoleDetials",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
