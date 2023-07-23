using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDidacticMaterialModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DidacticMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingsCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AverageRating = table.Column<decimal>(type: "decimal(4,3)", precision: 4, scale: 3, nullable: false, defaultValue: 0m),
                    DidacticMaterialType = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DidacticMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DidacticMaterials_UniversityCourses_UniversityCourseId",
                        column: x => x.UniversityCourseId,
                        principalTable: "UniversityCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DidacticMaterials_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DidacticMaterialOpinions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Opinion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DidacticMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DidacticMaterialOpinions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DidacticMaterialOpinions_DidacticMaterials_DidacticMaterialId",
                        column: x => x.DidacticMaterialId,
                        principalTable: "DidacticMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DidacticMaterialOpinions_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DidacticMaterialOpinions_AuthorId",
                table: "DidacticMaterialOpinions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DidacticMaterialOpinions_DidacticMaterialId",
                table: "DidacticMaterialOpinions",
                column: "DidacticMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_DidacticMaterials_AuthorId",
                table: "DidacticMaterials",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DidacticMaterials_UniversityCourseId",
                table: "DidacticMaterials",
                column: "UniversityCourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DidacticMaterialOpinions");

            migrationBuilder.DropTable(
                name: "DidacticMaterials");
        }
    }
}
