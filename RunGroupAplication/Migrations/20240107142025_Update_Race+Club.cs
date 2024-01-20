using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroupAplication.Migrations
{
    public partial class Update_RaceClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Races");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Races",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "Twitter",
                table: "Races",
                newName: "LongDescription");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Clubs",
                newName: "ShortDescription");

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Clubs");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Races",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "LongDescription",
                table: "Races",
                newName: "Twitter");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Clubs",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Races",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Races",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
