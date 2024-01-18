using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristianBeauty.Migrations
{
    public partial class FixBannersEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "OrderNum",
                table: "Banners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrderNum",
                table: "Banners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
