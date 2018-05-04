using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheBookCave.Migrations
{
    public partial class TookOutUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserBookWishlistConnections",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserBookCartConnections",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "UserBookWishlistConnections",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "UserBookCartConnections",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: true),
                    DateJoinedID = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FavoriteBookID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsPremium = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_AdressModel_AddressID",
                        column: x => x.AddressID,
                        principalTable: "AdressModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_DateModel_DateJoinedID",
                        column: x => x.DateJoinedID,
                        principalTable: "DateModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DateJoinedID",
                table: "Users",
                column: "DateJoinedID");
        }
    }
}
