using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheBookCave.Migrations
{
    public partial class AddedPaymentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentInfoID",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentInfoModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CVC = table.Column<string>(nullable: true),
                    CardHolder = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfoModel", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentInfoID",
                table: "Orders",
                column: "PaymentInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentInfoModel_PaymentInfoID",
                table: "Orders",
                column: "PaymentInfoID",
                principalTable: "PaymentInfoModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentInfoModel_PaymentInfoID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentInfoModel");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentInfoID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentInfoID",
                table: "Orders");
        }
    }
}
