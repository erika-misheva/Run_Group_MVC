using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroupAplication.Migrations
{
    public partial class AddAppUserClubTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Races",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserClub",
                columns: table => new
                {
                    ClubsId = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClub", x => new { x.ClubsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_AppUserClub_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserClub_Clubs_ClubsId",
                        column: x => x.ClubsId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClub_MembersId",
                table: "AppUserClub",
                column: "MembersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserClub");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Races",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
