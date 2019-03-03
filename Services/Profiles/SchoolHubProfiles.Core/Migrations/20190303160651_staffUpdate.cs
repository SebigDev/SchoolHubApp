using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolHubProfiles.Core.Migrations
{
    public partial class staffUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_Staff_StaffId",
                table: "Qualification");

            migrationBuilder.DropIndex(
                name: "IX_Qualification_StaffId",
                table: "Qualification");

            migrationBuilder.RenameColumn(
                name: "YearObtained",
                table: "Qualification",
                newName: "DateObtained");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Qualification",
                newName: "Certficate");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Staff",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Staff",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StaffId",
                table: "Qualification",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StaffQualificationMap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffQualificationMap", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffQualificationMap");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Staff");

            migrationBuilder.RenameColumn(
                name: "DateObtained",
                table: "Qualification",
                newName: "YearObtained");

            migrationBuilder.RenameColumn(
                name: "Certficate",
                table: "Qualification",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Staff",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StaffId",
                table: "Qualification",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_StaffId",
                table: "Qualification",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_Staff_StaffId",
                table: "Qualification",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
