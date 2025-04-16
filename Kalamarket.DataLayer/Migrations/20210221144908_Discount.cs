using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalamarket.DataLayer.Migrations
{
    public partial class Discount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_users_userid",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Cart_Cartid",
                table: "CartDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "cart");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_userid",
                table: "cart",
                newName: "IX_cart_userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart",
                table: "cart",
                column: "cartid");

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    discountid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discountcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discountpersent = table.Column<int>(type: "int", nullable: false),
                    Useablecount = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndCounttDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discounts", x => x.discountid);
                });

            migrationBuilder.CreateTable(
                name: "UserDiscounts",
                columns: table => new
                {
                    userdiscountid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    Discountid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDiscounts", x => x.userdiscountid);
                    table.ForeignKey(
                        name: "FK_UserDiscounts_discounts_Discountid",
                        column: x => x.Discountid,
                        principalTable: "discounts",
                        principalColumn: "discountid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDiscounts_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscounts_Discountid",
                table: "UserDiscounts",
                column: "Discountid");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscounts_userid",
                table: "UserDiscounts",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_users_userid",
                table: "cart",
                column: "userid",
                principalTable: "users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_cart_Cartid",
                table: "CartDetail",
                column: "Cartid",
                principalTable: "cart",
                principalColumn: "cartid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_users_userid",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_cart_Cartid",
                table: "CartDetail");

            migrationBuilder.DropTable(
                name: "UserDiscounts");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart",
                table: "cart");

            migrationBuilder.RenameTable(
                name: "cart",
                newName: "Cart");

            migrationBuilder.RenameIndex(
                name: "IX_cart_userid",
                table: "Cart",
                newName: "IX_Cart_userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "cartid");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_users_userid",
                table: "Cart",
                column: "userid",
                principalTable: "users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Cart_Cartid",
                table: "CartDetail",
                column: "Cartid",
                principalTable: "Cart",
                principalColumn: "cartid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
