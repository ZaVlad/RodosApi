using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHinges_Hinges_HingesId",
                table: "OrderHinges");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHinges_Orders_OrderId",
                table: "OrderHinges");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20c97c87-744c-4a17-8c21-f0cdf1458b20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91663e61-8bff-4d1b-85a0-bf8704d6a442");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50368bb3-2143-4fde-ae69-8a5ae2f9189f", "7fcf14fc-5b37-4079-a1de-54b9532ec07d", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23da16f3-9e5d-4509-abd6-645c76d9bd25", "cd594713-d1fa-4f7e-ae70-782a0c3edd1a", "User", null });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHinges_Hinges_HingesId",
                table: "OrderHinges",
                column: "HingesId",
                principalTable: "Hinges",
                principalColumn: "HingesId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHinges_Orders_OrderId",
                table: "OrderHinges",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHinges_Hinges_HingesId",
                table: "OrderHinges");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHinges_Orders_OrderId",
                table: "OrderHinges");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23da16f3-9e5d-4509-abd6-645c76d9bd25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50368bb3-2143-4fde-ae69-8a5ae2f9189f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20c97c87-744c-4a17-8c21-f0cdf1458b20", "aa396827-c22a-4492-8930-732c2994e01d", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91663e61-8bff-4d1b-85a0-bf8704d6a442", "47ae008f-fad9-4edd-ac4b-869acf4b6c1a", "User", null });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHinges_Hinges_HingesId",
                table: "OrderHinges",
                column: "HingesId",
                principalTable: "Hinges",
                principalColumn: "HingesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHinges_Orders_OrderId",
                table: "OrderHinges",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
