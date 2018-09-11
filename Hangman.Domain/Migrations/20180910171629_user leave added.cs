using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hangman.Domain.Migrations
{
    public partial class userleaveadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDatas");

            migrationBuilder.CreateTable(
                name: "UserLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityId = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLeaves_AspNetUsers_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLeaves_IdentityId",
                table: "UserLeaves",
                column: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLeaves");

            migrationBuilder.CreateTable(
                name: "GameDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    Hint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDatas", x => x.Id);
                });
        }
    }
}
