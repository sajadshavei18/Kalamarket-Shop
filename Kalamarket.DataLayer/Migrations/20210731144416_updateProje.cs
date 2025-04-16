using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalamarket.DataLayer.Migrations
{
    public partial class updateProje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "posted",
                table: "cart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "posted",
                table: "cart");
        }
    }
}
