using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAPI101_Back.Migrations
{
    public partial class UserAlternativeKeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Login",
                table: "Users",
                column: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Login",
                table: "Users");
        }
    }
}
