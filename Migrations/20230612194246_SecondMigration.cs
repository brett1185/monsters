using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monsters.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Monsters",
                newName: "LastSeen");

            migrationBuilder.AddColumn<string>(
                name: "LocatedAt",
                table: "Monsters",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocatedAt",
                table: "Monsters");

            migrationBuilder.RenameColumn(
                name: "LastSeen",
                table: "Monsters",
                newName: "Height");
        }
    }
}
