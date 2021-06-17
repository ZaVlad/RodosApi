using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class AddManyToManyForDoorHandle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "231de5a1-2a5d-4743-93e0-8dfeca509a90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70ae0dba-5919-4672-a828-e40efbf21300");

            migrationBuilder.AlterColumn<int>(
                name: "DoorHandleQuantity",
                table: "OrderDoorHandles",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24ee23ee-716d-4ff0-ad81-51bec726f151", "ee14ae3c-b91e-4459-9841-0d0ae7431980", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f54952b9-0eeb-4c59-b63d-cad7e937535d", "1278d288-a061-4c74-84a4-27ee3718e007", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24ee23ee-716d-4ff0-ad81-51bec726f151");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f54952b9-0eeb-4c59-b63d-cad7e937535d");

            migrationBuilder.AlterColumn<long>(
                name: "DoorHandleQuantity",
                table: "OrderDoorHandles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70ae0dba-5919-4672-a828-e40efbf21300", "da9f6067-d80d-4743-abf2-b7a54a4ffc9f", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "231de5a1-2a5d-4743-93e0-8dfeca509a90", "45838546-796b-47d0-bcaf-9fdff985504a", "User", null });
        }
    }
}
