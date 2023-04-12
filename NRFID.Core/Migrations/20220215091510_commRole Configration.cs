using Microsoft.EntityFrameworkCore.Migrations;

namespace NFC.Core.Migrations
{
    public partial class commRoleConfigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommRoleDetials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CommRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommRoleDetials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommRoleDetials_CommRoles_CommRoleId",
                        column: x => x.CommRoleId,
                        principalTable: "CommRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommRoleDetials_CommRoleId",
                table: "CommRoleDetials",
                column: "CommRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommRoleDetials");

            migrationBuilder.DropTable(
                name: "CommRoles");
        }
    }
}
