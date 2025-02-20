using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace AquaFlow.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSpatialIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "FishFarms");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "FishFarms");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "FishFarms",
                type: "geography",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "FishFarms");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "FishFarms",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "FishFarms",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
