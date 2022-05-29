using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEOsign.Migrations
{
    public partial class addCompanyMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Societes_Users_UserId",
                table: "Societes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Societes",
                table: "Societes");

            migrationBuilder.RenameTable(
                name: "Societes",
                newName: "Societe");

            migrationBuilder.RenameIndex(
                name: "IX_Societes_UserId",
                table: "Societe",
                newName: "IX_Societe_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Societe",
                table: "Societe",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Societe_Users_UserId",
                table: "Societe",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Societe_Users_UserId",
                table: "Societe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Societe",
                table: "Societe");

            migrationBuilder.RenameTable(
                name: "Societe",
                newName: "Societes");

            migrationBuilder.RenameIndex(
                name: "IX_Societe_UserId",
                table: "Societes",
                newName: "IX_Societes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Societes",
                table: "Societes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Societes_Users_UserId",
                table: "Societes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
