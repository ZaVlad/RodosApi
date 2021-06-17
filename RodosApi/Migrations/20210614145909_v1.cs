using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RodosApi.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Coatings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    CollectionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryStatuses",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoorModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureTypes",
                columns: table => new
                {
                    FurnitureId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureTypes", x => x.FurnitureId);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfDoors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfDoors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfHinges",
                columns: table => new
                {
                    TypeOfHingeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfHinges", x => x.TypeOfHingeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Makers",
                columns: table => new
                {
                    MakerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makers", x => x.MakerId);
                    table.ForeignKey(
                        name: "FK_Makers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoorHandles",
                columns: table => new
                {
                    DoorHandleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    MakerId = table.Column<long>(type: "bigint", nullable: false),
                    FurnitureTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ColorId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorHandles", x => x.DoorHandleId);
                    table.ForeignKey(
                        name: "FK_DoorHandles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorHandles_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorHandles_FurnitureTypes_FurnitureTypeId",
                        column: x => x.FurnitureTypeId,
                        principalTable: "FurnitureTypes",
                        principalColumn: "FurnitureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorHandles_Makers_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Makers",
                        principalColumn: "MakerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorHandles_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hinges",
                columns: table => new
                {
                    HingesId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    TypeOfHingesId = table.Column<long>(type: "bigint", nullable: false),
                    MakerId = table.Column<long>(type: "bigint", nullable: false),
                    FurnitureTypeId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hinges", x => x.HingesId);
                    table.ForeignKey(
                        name: "FK_Hinges_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hinges_FurnitureTypes_FurnitureTypeId",
                        column: x => x.FurnitureTypeId,
                        principalTable: "FurnitureTypes",
                        principalColumn: "FurnitureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hinges_Makers_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Makers",
                        principalColumn: "MakerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hinges_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hinges_TypesOfHinges_TypeOfHingesId",
                        column: x => x.TypeOfHingesId,
                        principalTable: "TypesOfHinges",
                        principalColumn: "TypeOfHingeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    DoorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    DoorModelId = table.Column<long>(type: "bigint", nullable: false),
                    CoatingId = table.Column<long>(type: "bigint", nullable: false),
                    CollectionId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ColorId = table.Column<long>(type: "bigint", nullable: false),
                    MakerId = table.Column<long>(type: "bigint", nullable: false),
                    DoorHandleId = table.Column<long>(type: "bigint", nullable: false),
                    HingesId = table.Column<long>(type: "bigint", nullable: false),
                    TypeOfDoorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.DoorId);
                    table.ForeignKey(
                        name: "FK_Doors_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doors_Coatings_CoatingId",
                        column: x => x.CoatingId,
                        principalTable: "Coatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doors_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId");
                    table.ForeignKey(
                        name: "FK_Doors_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doors_DoorHandles_DoorHandleId",
                        column: x => x.DoorHandleId,
                        principalTable: "DoorHandles",
                        principalColumn: "DoorHandleId");
                    table.ForeignKey(
                        name: "FK_Doors_DoorModels_DoorModelId",
                        column: x => x.DoorModelId,
                        principalTable: "DoorModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doors_Hinges_HingesId",
                        column: x => x.HingesId,
                        principalTable: "Hinges",
                        principalColumn: "HingesId");
                    table.ForeignKey(
                        name: "FK_Doors_Makers_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Makers",
                        principalColumn: "MakerId");
                    table.ForeignKey(
                        name: "FK_Doors_TypesOfDoors_TypeOfDoorId",
                        column: x => x.TypeOfDoorId,
                        principalTable: "TypesOfDoors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    DoorId = table.Column<long>(type: "bigint", nullable: true),
                    DoorHandleId = table.Column<long>(type: "bigint", nullable: true),
                    DoorQuantity = table.Column<byte>(type: "tinyint", nullable: false),
                    HingesQuantity = table.Column<byte>(type: "tinyint", nullable: false),
                    DoorHandleQuantity = table.Column<byte>(type: "tinyint", nullable: false),
                    DeliveryStatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryStatuses_DeliveryStatusId",
                        column: x => x.DeliveryStatusId,
                        principalTable: "DeliveryStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_DoorHandles_DoorHandleId",
                        column: x => x.DoorHandleId,
                        principalTable: "DoorHandles",
                        principalColumn: "DoorHandleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Doors_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "DoorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderHinges",
                columns: table => new
                {
                    HingesId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    HingesQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHinges", x => new { x.HingesId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderHinges_Hinges_HingesId",
                        column: x => x.HingesId,
                        principalTable: "Hinges",
                        principalColumn: "HingesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHinges_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20c97c87-744c-4a17-8c21-f0cdf1458b20", "aa396827-c22a-4492-8930-732c2994e01d", "Admin", null },
                    { "91663e61-8bff-4d1b-85a0-bf8704d6a442", "47ae008f-fad9-4edd-ac4b-869acf4b6c1a", "User", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1L, "Furniture" },
                    { 2L, "Door" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "Email", "LastName", "Name", "Phone" },
                values: new object[,]
                {
                    { 1L, "ms 1 street", "OlegKick@ukr.net", "Krigan", "Oleg", "380500653293" },
                    { 2L, "BubleCity5 street", "ADventureTime@ukr.net", "Mortens", "Finn", "" }
                });

            migrationBuilder.InsertData(
                table: "Coatings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Metal" },
                    { 2L, "Wood" }
                });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "Name" },
                values: new object[,]
                {
                    { 1L, "Basic street" },
                    { 2L, "White king" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Red" },
                    { 2L, "Blue" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { 2L, "Italia" },
                    { 1L, "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Preparing for delivery" },
                    { (byte)2, "In delivery process" },
                    { (byte)3, "Was delivered" }
                });

            migrationBuilder.InsertData(
                table: "DoorModels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Bas 001" },
                    { 2L, "White winter 002" }
                });

            migrationBuilder.InsertData(
                table: "FurnitureTypes",
                columns: new[] { "FurnitureId", "Name" },
                values: new object[,]
                {
                    { 1L, "Door Handle" },
                    { 2L, "Hinges" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "MaterialId", "Name" },
                values: new object[,]
                {
                    { 1L, "Chrome" },
                    { 2L, "Diamond" }
                });

            migrationBuilder.InsertData(
                table: "TypesOfDoors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Entrance door" },
                    { 2L, "Interior door" }
                });

            migrationBuilder.InsertData(
                table: "TypesOfHinges",
                columns: new[] { "TypeOfHingeId", "Name" },
                values: new object[,]
                {
                    { 1L, "Mortise looks" },
                    { 2L, "Other looks" }
                });

            migrationBuilder.InsertData(
                table: "Makers",
                columns: new[] { "MakerId", "CountryId", "Name" },
                values: new object[] { 1L, 1L, "Rodos" });

            migrationBuilder.InsertData(
                table: "Makers",
                columns: new[] { "MakerId", "CountryId", "Name" },
                values: new object[] { 2L, 2L, "Mario" });

            migrationBuilder.InsertData(
                table: "DoorHandles",
                columns: new[] { "DoorHandleId", "CategoryId", "ColorId", "FurnitureTypeId", "MakerId", "MaterialId", "Name", "Price" },
                values: new object[,]
                {
                    { 2L, 1L, 2L, 1L, 1L, 2L, "Door handle water+ Rodos", 450.54m },
                    { 1L, 1L, 1L, 1L, 2L, 1L, "Forme Door handle italia", 245.34m }
                });

            migrationBuilder.InsertData(
                table: "Hinges",
                columns: new[] { "HingesId", "CategoryId", "FurnitureTypeId", "MakerId", "MaterialId", "Name", "Price", "TypeOfHingesId" },
                values: new object[,]
                {
                    { 2L, 1L, 1L, 1L, 2L, "Deffault look", 104.94m, 1L },
                    { 1L, 1L, 1L, 2L, 1L, "Magnetic look", 700.23m, 2L }
                });

            migrationBuilder.InsertData(
                table: "Doors",
                columns: new[] { "DoorId", "CategoryId", "CoatingId", "CollectionId", "ColorId", "Description", "DoorHandleId", "DoorModelId", "HingesId", "MakerId", "Name", "Price", "TypeOfDoorId" },
                values: new object[] { 1L, 2L, 1L, 1L, 2L, "White death door", 1L, 1L, 1L, 2L, "winter aid", 5000.72m, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DoorHandles_CategoryId",
                table: "DoorHandles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorHandles_ColorId",
                table: "DoorHandles",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorHandles_FurnitureTypeId",
                table: "DoorHandles",
                column: "FurnitureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorHandles_MakerId",
                table: "DoorHandles",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorHandles_MaterialId",
                table: "DoorHandles",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_CategoryId",
                table: "Doors",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_CoatingId",
                table: "Doors",
                column: "CoatingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_CollectionId",
                table: "Doors",
                column: "CollectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_ColorId",
                table: "Doors",
                column: "ColorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_DoorHandleId",
                table: "Doors",
                column: "DoorHandleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_DoorModelId",
                table: "Doors",
                column: "DoorModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_HingesId",
                table: "Doors",
                column: "HingesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_MakerId",
                table: "Doors",
                column: "MakerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_TypeOfDoorId",
                table: "Doors",
                column: "TypeOfDoorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hinges_CategoryId",
                table: "Hinges",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hinges_FurnitureTypeId",
                table: "Hinges",
                column: "FurnitureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hinges_MakerId",
                table: "Hinges",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hinges_MaterialId",
                table: "Hinges",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Hinges_TypeOfHingesId",
                table: "Hinges",
                column: "TypeOfHingesId");

            migrationBuilder.CreateIndex(
                name: "IX_Makers_CountryId",
                table: "Makers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHinges_OrderId",
                table: "OrderHinges",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryStatusId",
                table: "Orders",
                column: "DeliveryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DoorHandleId",
                table: "Orders",
                column: "DoorHandleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DoorId",
                table: "Orders",
                column: "DoorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderHinges");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "DeliveryStatuses");

            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropTable(
                name: "Coatings");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "DoorHandles");

            migrationBuilder.DropTable(
                name: "DoorModels");

            migrationBuilder.DropTable(
                name: "Hinges");

            migrationBuilder.DropTable(
                name: "TypesOfDoors");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FurnitureTypes");

            migrationBuilder.DropTable(
                name: "Makers");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "TypesOfHinges");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
