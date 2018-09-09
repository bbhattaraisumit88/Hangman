using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hangman.Domain.Migrations
{
    public partial class GameDataModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Hint = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDatas");
        }
    }
}
