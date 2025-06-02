using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPlay.Migrations
{
    /// <inheritdoc />
    public partial class addtrailer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Trailer",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trailer",
                table: "Games");
        }
    }
}
