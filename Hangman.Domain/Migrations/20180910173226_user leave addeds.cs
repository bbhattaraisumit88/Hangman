using Microsoft.EntityFrameworkCore.Migrations;

namespace Hangman.Domain.Migrations
{
    public partial class userleaveaddeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLeaves_AspNetUsers_IdentityId",
                table: "UserLeaves");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserLeaves");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "UserLeaves");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "UserLeaves",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "UserLeaves",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "UserLeaves",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "UserLeaves",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLeaves_AspNetUsers_IdentityId",
                table: "UserLeaves",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLeaves_AspNetUsers_IdentityId",
                table: "UserLeaves");

            migrationBuilder.DropColumn(
                name: "From",
                table: "UserLeaves");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "UserLeaves");

            migrationBuilder.DropColumn(
                name: "To",
                table: "UserLeaves");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "UserLeaves",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UserLeaves",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "UserLeaves",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLeaves_AspNetUsers_IdentityId",
                table: "UserLeaves",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
