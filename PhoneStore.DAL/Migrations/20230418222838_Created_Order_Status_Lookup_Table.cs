using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStore.DAL.Migrations
{
    public partial class Created_Order_Status_Lookup_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderStatusWorkflows");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "OrderStatusWorkflows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderStatusesLookup",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusesLookup", x => x.OrderStatusId);
                });

            migrationBuilder.InsertData(
                table: "OrderStatusesLookup",
                columns: new[] { "OrderStatusId", "Status" },
                values: new object[,]
                {
                    { 0, "Open" },
                    { 1, "Closed" },
                    { 2, "Paid" },
                    { 3, "Delivered" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusWorkflows_OrderStatusId",
                table: "OrderStatusWorkflows",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatusesLookup_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusWorkflows_OrderStatusesLookup_OrderStatusId",
                table: "OrderStatusWorkflows");

            migrationBuilder.DropTable(
                name: "OrderStatusesLookup");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatusWorkflows_OrderStatusId",
                table: "OrderStatusWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "OrderStatusWorkflows");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderStatusWorkflows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
