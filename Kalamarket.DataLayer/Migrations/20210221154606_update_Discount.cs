using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalamarket.DataLayer.Migrations
{
    public partial class update_Discount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndCounttDate",
                table: "discounts",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "discounts",
                newName: "EndCounttDate");
        }
    }
}
