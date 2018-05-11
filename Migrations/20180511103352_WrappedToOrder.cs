using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheBookCave.Migrations
{
    public partial class WrappedToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentOrder",
                table: "Orders",
                newName: "IsWrapped");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentOrder",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentOrder",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "IsWrapped",
                table: "Orders",
                newName: "CurrentOrder");
        }
    }
}
