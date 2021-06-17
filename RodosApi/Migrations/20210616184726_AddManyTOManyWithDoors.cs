using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class AddManyTOManyWithDoors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Doors_DoorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DoorId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0992e1e8-3498-464a-9b15-0153fe099276");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e88431c1-5f55-49cd-a42d-6bb271303da6");

            migrationBuilder.DropColumn(
                name: "DoorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DoorQuantity",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderDoors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoorId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDoors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDoors_Doors_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "DoorId");
                    table.ForeignKey(
                        name: "FK_OrderDoors_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "74889780-5625-4320-b634-fb1cea82434a", "13b63037-a37c-4bf6-9f97-ae50e7fde56a", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "107b0e52-2b77-4250-9d49-009ab670c9be", "6587f260-2abb-465f-8a85-f2a240a2cbe5", "User", null });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDoors_DoorId",
                table: "OrderDoors",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDoors_OrderId",
                table: "OrderDoors",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDoors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "107b0e52-2b77-4250-9d49-009ab670c9be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74889780-5625-4320-b634-fb1cea82434a");

            migrationBuilder.AddColumn<long>(
                name: "DoorId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "DoorQuantity",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e88431c1-5f55-49cd-a42d-6bb271303da6", "da131a30-4af7-46b9-9d94-ef232c219fb5", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0992e1e8-3498-464a-9b15-0153fe099276", "48b4e8b5-f10f-40e5-a0d4-0f7c33df0ee6", "User", null });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DoorId",
                table: "Orders",
                column: "DoorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Doors_DoorId",
                table: "Orders",
                column: "DoorId",
                principalTable: "Doors",
                principalColumn: "DoorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
