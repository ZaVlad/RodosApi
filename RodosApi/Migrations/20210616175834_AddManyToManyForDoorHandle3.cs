using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class AddManyToManyForDoorHandle3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24ee23ee-716d-4ff0-ad81-51bec726f151");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f54952b9-0eeb-4c59-b63d-cad7e937535d");

            migrationBuilder.DropColumn(
                name: "DoorHandleQuantity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HingesQuantity",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e88431c1-5f55-49cd-a42d-6bb271303da6", "da131a30-4af7-46b9-9d94-ef232c219fb5", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0992e1e8-3498-464a-9b15-0153fe099276", "48b4e8b5-f10f-40e5-a0d4-0f7c33df0ee6", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0992e1e8-3498-464a-9b15-0153fe099276");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e88431c1-5f55-49cd-a42d-6bb271303da6");

            migrationBuilder.AddColumn<byte>(
                name: "DoorHandleQuantity",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "HingesQuantity",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24ee23ee-716d-4ff0-ad81-51bec726f151", "ee14ae3c-b91e-4459-9841-0d0ae7431980", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f54952b9-0eeb-4c59-b63d-cad7e937535d", "1278d288-a061-4c74-84a4-27ee3718e007", "User", null });
        }
    }
}
