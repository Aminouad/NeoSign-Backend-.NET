using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEOsign.Migrations
{
    public partial class updatePersonnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Master",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Master",
                table: "Users");
        }
    }
}
