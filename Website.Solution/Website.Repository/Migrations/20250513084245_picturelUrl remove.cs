using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Repository.Migrations
{
    /// <inheritdoc />
    public partial class picturelUrlremove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVirtual",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VirtualMeetingUrl",
                table: "Events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVirtual",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VirtualMeetingUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
