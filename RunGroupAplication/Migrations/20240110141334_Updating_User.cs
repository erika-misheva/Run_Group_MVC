using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroupAplication.Migrations
{
    public partial class Updating_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info",
                table: "AspNetUsers");
        }
    }
}
