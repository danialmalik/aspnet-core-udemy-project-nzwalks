using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_core_web_api_udemy.Migrations
{
    /// <inheritdoc />
    public partial class RenamecolumnLengthforWalk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Walks",
                newName: "LengthInKm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LengthInKm",
                table: "Walks",
                newName: "Length");
        }
    }
}
