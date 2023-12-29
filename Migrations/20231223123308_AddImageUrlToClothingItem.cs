using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabFinal.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToClothingItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ClothingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "ClothingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ClothingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ClothingItems");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ClothingItems");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ClothingItems");
        }
    }
}
