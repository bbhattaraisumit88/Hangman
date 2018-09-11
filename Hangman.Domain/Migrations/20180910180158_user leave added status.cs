using Microsoft.EntityFrameworkCore.Migrations;

namespace Hangman.Domain.Migrations
{
    public partial class userleaveaddedstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "UserLeaves",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserLeaves");
        }
    }
}
