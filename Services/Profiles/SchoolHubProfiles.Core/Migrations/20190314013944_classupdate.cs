using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolHubProfiles.Core.Migrations
{
    public partial class classupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "ClassName",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ClassName");
        }
    }
}
