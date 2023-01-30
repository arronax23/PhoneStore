using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStore.DAL.Migrations
{
    public partial class PasswordHash_Introduced : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "ApplicationUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
