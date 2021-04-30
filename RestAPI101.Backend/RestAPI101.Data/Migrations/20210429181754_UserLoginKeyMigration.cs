using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAPI101.Data.Migrations
{
    public partial class UserLoginKeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Labels_UserId",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Labels");

            migrationBuilder.AddColumn<string>(
                name: "UserLogin",
                table: "Todos",
                type: "character varying(32)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserLogin",
                table: "Labels",
                type: "character varying(32)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Login",
                table: "Users",
                column: "Login");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserLogin",
                table: "Todos",
                column: "UserLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_UserLogin",
                table: "Labels",
                column: "UserLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_UserLogin",
                table: "Labels",
                column: "UserLogin",
                principalTable: "Users",
                principalColumn: "Login",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserLogin",
                table: "Todos",
                column: "UserLogin",
                principalTable: "Users",
                principalColumn: "Login",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_UserLogin",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserLogin",
                table: "Todos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Login",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserLogin",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Labels_UserLogin",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "UserLogin",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserLogin",
                table: "Labels");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Todos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Labels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserId",
                table: "Todos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_UserId",
                table: "Labels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
