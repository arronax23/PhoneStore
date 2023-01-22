using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStore.DAL.Migrations
{
    public partial class PropertiesAddedToPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Phones");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Phones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Camera",
                table: "Phones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Phones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Phones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Memory",
                table: "Phones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Phones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "Phones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RAM",
                table: "Phones",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Camera",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Memory",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "RAM",
                table: "Phones");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
