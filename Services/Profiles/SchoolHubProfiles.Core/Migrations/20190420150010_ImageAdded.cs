using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolHubProfiles.Core.Migrations
{
    public partial class ImageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Student",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Staff",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Student",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Staff",
                newName: "Photo");
        }
    }
}
