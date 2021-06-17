using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class AddManyTOManyWithDoors2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "107b0e52-2b77-4250-9d49-009ab670c9be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74889780-5625-4320-b634-fb1cea82434a");

            migrationBuilder.RenameColumn(
                name: "OrderQuantity",
                table: "OrderDoors",
                newName: "DoorQuantity");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "beb5bf06-1ac7-4e03-bcf6-12c57463d204", "dd795611-c7f7-40e7-aea5-f959befa49b7", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "466154c0-0444-45a2-a36f-804c3af5ff3f", "d8673fe1-8990-4a56-b6cc-3220ca7d6e32", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "466154c0-0444-45a2-a36f-804c3af5ff3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "beb5bf06-1ac7-4e03-bcf6-12c57463d204");

            migrationBuilder.RenameColumn(
                name: "DoorQuantity",
                table: "OrderDoors",
                newName: "OrderQuantity");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "74889780-5625-4320-b634-fb1cea82434a", "13b63037-a37c-4bf6-9f97-ae50e7fde56a", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "107b0e52-2b77-4250-9d49-009ab670c9be", "6587f260-2abb-465f-8a85-f2a240a2cbe5", "User", null });
        }
    }
}
