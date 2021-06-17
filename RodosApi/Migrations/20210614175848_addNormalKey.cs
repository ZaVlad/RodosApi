using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class addNormalKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHinges",
                table: "OrderHinges");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23da16f3-9e5d-4509-abd6-645c76d9bd25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50368bb3-2143-4fde-ae69-8a5ae2f9189f");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "OrderHinges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHinges",
                table: "OrderHinges",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6d0aee71-d6bf-4c77-b8f1-6139e3738030", "746bf3ba-dfbe-41ec-9cb4-f7e5de8fb1a1", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b14a24b1-9c26-4e68-8cea-cd0bb6c5267c", "5738f9a9-52aa-4042-91cd-233d6c250a25", "User", null });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHinges_HingesId",
                table: "OrderHinges",
                column: "HingesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHinges",
                table: "OrderHinges");

            migrationBuilder.DropIndex(
                name: "IX_OrderHinges_HingesId",
                table: "OrderHinges");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d0aee71-d6bf-4c77-b8f1-6139e3738030");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b14a24b1-9c26-4e68-8cea-cd0bb6c5267c");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderHinges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHinges",
                table: "OrderHinges",
                columns: new[] { "HingesId", "OrderId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50368bb3-2143-4fde-ae69-8a5ae2f9189f", "7fcf14fc-5b37-4079-a1de-54b9532ec07d", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23da16f3-9e5d-4509-abd6-645c76d9bd25", "cd594713-d1fa-4f7e-ae70-782a0c3edd1a", "User", null });
        }
    }
}
