using Microsoft.EntityFrameworkCore.Migrations;

namespace NFC.Core.Migrations
{
    public partial class Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "applicationGroupSettingId",
                table: "ApplicationSettings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSettings_applicationGroupSettingId",
                table: "ApplicationSettings",
                column: "applicationGroupSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSettings_ApplicationGroupSettings_applicationGroupSettingId",
                table: "ApplicationSettings",
                column: "applicationGroupSettingId",
                principalTable: "ApplicationGroupSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSettings_ApplicationGroupSettings_applicationGroupSettingId",
                table: "ApplicationSettings");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationSettings_applicationGroupSettingId",
                table: "ApplicationSettings");

            migrationBuilder.DropColumn(
                name: "applicationGroupSettingId",
                table: "ApplicationSettings");
        }
    }
}
