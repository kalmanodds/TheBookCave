using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheBookCave.Migrations
{
    public partial class AddedUsersAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AdressModel_ShippingAddressID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "AdressModel");

            migrationBuilder.CreateTable(
                name: "AddressModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    Zip = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FavoriteBookID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsPremium = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_AddressModel_AddressID",
                        column: x => x.AddressID,
                        principalTable: "AddressModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AddressModel_ShippingAddressID",
                table: "Orders",
                column: "ShippingAddressID",
                principalTable: "AddressModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AddressModel_ShippingAddressID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AddressModel");

            migrationBuilder.CreateTable(
                name: "AdressModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    Zip = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdressModel", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AdressModel_ShippingAddressID",
                table: "Orders",
                column: "ShippingAddressID",
                principalTable: "AdressModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
