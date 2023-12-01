using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAverageRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "ExerciseSolutions",
                type: "decimal(4,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "Exercises",
                type: "decimal(4,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "DidacticMaterials",
                type: "decimal(4,3)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "ExerciseSolutions");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "DidacticMaterials");
        }
    }
}
