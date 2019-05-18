using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolHubProfiles.Core.Migrations
{
    public partial class ImagePathAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Staff");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Staff",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Staff");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Staff",
                nullable: true);
        }
    }
}
