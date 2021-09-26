using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneShop.DAL.Migrations
{
    public partial class AddedOrderStatusWorkflowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneOrder_Order_OrderId",
                table: "PhoneOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneOrder_Phones_PhoneId",
                table: "PhoneOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneOrder",
                table: "PhoneOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "PhoneOrder",
                newName: "PhoneOrders");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneOrder_PhoneId",
                table: "PhoneOrders",
                newName: "IX_PhoneOrders_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneOrder_OrderId",
                table: "PhoneOrders",
                newName: "IX_PhoneOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_UserId",
                table: "Customers",
                newName: "IX_Customers_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneOrders",
                table: "PhoneOrders",
                column: "PhoneOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.CreateTable(
                name: "OrderStatusWorkflows",
                columns: table => new
                {
                    OrderStatusWorkflowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    WorkflowDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusWorkflows", x => x.OrderStatusWorkflowId);
                    table.ForeignKey(
                        name: "FK_OrderStatusWorkflows_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusWorkflows_OrderId",
                table: "OrderStatusWorkflows",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneOrders_Orders_OrderId",
                table: "PhoneOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneOrders_Phones_PhoneId",
                table: "PhoneOrders",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "PhoneId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneOrders_Orders_OrderId",
                table: "PhoneOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneOrders_Phones_PhoneId",
                table: "PhoneOrders");

            migrationBuilder.DropTable(
                name: "OrderStatusWorkflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneOrders",
                table: "PhoneOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "PhoneOrders",
                newName: "PhoneOrder");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneOrders_PhoneId",
                table: "PhoneOrder",
                newName: "IX_PhoneOrder_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneOrders_OrderId",
                table: "PhoneOrder",
                newName: "IX_PhoneOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserId",
                table: "Customer",
                newName: "IX_Customer_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneOrder",
                table: "PhoneOrder",
                column: "PhoneOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneOrder_Order_OrderId",
                table: "PhoneOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneOrder_Phones_PhoneId",
                table: "PhoneOrder",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "PhoneId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
