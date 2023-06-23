using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristianBeauty.Migrations
{
    public partial class AddIsEnableToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "Products");
        }
    }
}
