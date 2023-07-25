using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableForRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "DidacticMaterials");

            migrationBuilder.DropColumn(
                name: "RatingsCount",
                table: "DidacticMaterials");

            migrationBuilder.CreateTable(
                name: "DidacticMaterialRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DidacticMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DidacticMaterialRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DidacticMaterialRatings_DidacticMaterials_DidacticMaterialId",
                        column: x => x.DidacticMaterialId,
                        principalTable: "DidacticMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DidacticMaterialRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DidacticMaterialRatings_DidacticMaterialId",
                table: "DidacticMaterialRatings",
                column: "DidacticMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_DidacticMaterialRatings_UserId",
                table: "DidacticMaterialRatings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DidacticMaterialRatings");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "DidacticMaterials",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RatingsCount",
                table: "DidacticMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
