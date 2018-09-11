using Microsoft.EntityFrameworkCore.Migrations;

namespace Hangman.Domain.Migrations
{
    public partial class userleaveaddedstatuscolumnchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserLeaves",
                nullable: true,
                defaultValue: "unapproved",
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "UserLeaves",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
