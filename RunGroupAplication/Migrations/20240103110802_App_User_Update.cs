using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroupAplication.Migrations
{
    public partial class App_User_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MileAge",
                table: "AspNetUsers",
                newName: "Mileage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mileage",
                table: "AspNetUsers",
                newName: "MileAge");
        }
    }
}
