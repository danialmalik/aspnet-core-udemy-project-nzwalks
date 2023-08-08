using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_core_web_api_udemy.Migrations
{
    /// <inheritdoc />
    public partial class RenamecolumnImgUrlforWalkandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkImgUrl",
                table: "Walks",
                newName: "WalkImageUrl");

            migrationBuilder.RenameColumn(
                name: "RegionImgUrl",
                table: "Regions",
                newName: "RegionImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkImageUrl",
                table: "Walks",
                newName: "WalkImgUrl");

            migrationBuilder.RenameColumn(
                name: "RegionImageUrl",
                table: "Regions",
                newName: "RegionImgUrl");
        }
    }
}
