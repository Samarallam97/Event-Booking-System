using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Repository.Migrations
{
    /// <inheritdoc />
    public partial class arabicFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameAR",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationAR",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescriptionAR",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusAR",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleAR",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAR",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullNameAR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAR",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocationAR",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ShortDescriptionAR",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StatusAR",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TitleAR",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "NameAR",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FullNameAR",
                table: "AspNetUsers");
        }
    }
}
