using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class AddManyToManyForDoorHandle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DoorHandles_DoorHandleId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DoorHandleId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d0aee71-d6bf-4c77-b8f1-6139e3738030");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b14a24b1-9c26-4e68-8cea-cd0bb6c5267c");

            migrationBuilder.DropColumn(
                name: "DoorHandleId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderDoorHandles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoorHandleId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    DoorHandleQuantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDoorHandles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDoorHandles_DoorHandles_DoorHandleId",
                        column: x => x.DoorHandleId,
                        principalTable: "DoorHandles",
                        principalColumn: "DoorHandleId");
                    table.ForeignKey(
                        name: "FK_OrderDoorHandles_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70ae0dba-5919-4672-a828-e40efbf21300", "da9f6067-d80d-4743-abf2-b7a54a4ffc9f", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "231de5a1-2a5d-4743-93e0-8dfeca509a90", "45838546-796b-47d0-bcaf-9fdff985504a", "User", null });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDoorHandles_DoorHandleId",
                table: "OrderDoorHandles",
                column: "DoorHandleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDoorHandles_OrderId",
                table: "OrderDoorHandles",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDoorHandles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "231de5a1-2a5d-4743-93e0-8dfeca509a90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70ae0dba-5919-4672-a828-e40efbf21300");

            migrationBuilder.AddColumn<long>(
                name: "DoorHandleId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6d0aee71-d6bf-4c77-b8f1-6139e3738030", "746bf3ba-dfbe-41ec-9cb4-f7e5de8fb1a1", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b14a24b1-9c26-4e68-8cea-cd0bb6c5267c", "5738f9a9-52aa-4042-91cd-233d6c250a25", "User", null });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DoorHandleId",
                table: "Orders",
                column: "DoorHandleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DoorHandles_DoorHandleId",
                table: "Orders",
                column: "DoorHandleId",
                principalTable: "DoorHandles",
                principalColumn: "DoorHandleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
