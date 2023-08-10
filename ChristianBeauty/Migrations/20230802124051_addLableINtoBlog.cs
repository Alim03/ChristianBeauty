using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristianBeauty.Migrations
{
    public partial class addLableINtoBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Labels",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Labels",
                table: "Blogs");
        }
    }
}
