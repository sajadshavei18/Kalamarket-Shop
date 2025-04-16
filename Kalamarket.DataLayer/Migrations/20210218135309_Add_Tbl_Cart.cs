using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalamarket.DataLayer.Migrations
{
    public partial class Add_Tbl_Cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    cartid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    ispay = table.Column<bool>(type: "bit", nullable: false),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.cartid);
                    table.ForeignKey(
                        name: "FK_Cart_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartDetail",
                columns: table => new
                {
                    CartDetailid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productid = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cartid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetail", x => x.CartDetailid);
                    table.ForeignKey(
                        name: "FK_CartDetail_Cart_Cartid",
                        column: x => x.Cartid,
                        principalTable: "Cart",
                        principalColumn: "cartid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_userid",
                table: "Cart",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_Cartid",
                table: "CartDetail",
                column: "Cartid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartDetail");

            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
