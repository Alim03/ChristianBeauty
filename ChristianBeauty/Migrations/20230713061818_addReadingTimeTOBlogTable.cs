﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristianBeauty.Migrations
{
    public partial class addReadingTimeTOBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadingTime",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingTime",
                table: "Blogs");
        }
    }
}
