using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStore.DAL.Migrations
{
    public partial class Rename_OrderStatusesWorkflows_Table_To_OrderStatusWorkflows_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatusesLookup_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusWorkflows_OrderStatusesLookup_OrderStatusId",
                table: "OrderStatusWorkflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatusesLookup",
                table: "OrderStatusesLookup");

            migrationBuilder.RenameTable(
                name: "OrderStatusesLookup",
                newName: "OrderStatusLookup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatusLookup",
                table: "OrderStatusLookup",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatusLookup_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatusLookup",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusWorkflows_OrderStatusLookup_OrderStatusId",
                table: "OrderStatusWorkflows",
                column: "OrderStatusId",
                principalTable: "OrderStatusLookup",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.NoAction);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatusLookup_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusWorkflows_OrderStatusLookup_OrderStatusId",
                table: "OrderStatusWorkflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatusLookup",
                table: "OrderStatusLookup");

            migrationBuilder.RenameTable(
                name: "OrderStatusLookup",
                newName: "OrderStatusesLookup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatusesLookup",
                table: "OrderStatusesLookup",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatusesLookup_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatusesLookup",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusWorkflows_OrderStatusesLookup_OrderStatusId",
                table: "OrderStatusWorkflows",
                column: "OrderStatusId",
                principalTable: "OrderStatusesLookup",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
