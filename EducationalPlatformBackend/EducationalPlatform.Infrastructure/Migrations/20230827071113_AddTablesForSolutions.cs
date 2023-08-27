using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesForSolutions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseSolutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSolutions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseSolutions_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSolutionRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseSolutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(4,3)", precision: 4, scale: 3, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSolutionRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSolutionRatings_ExerciseSolutions_ExerciseSolutionId",
                        column: x => x.ExerciseSolutionId,
                        principalTable: "ExerciseSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseSolutionRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSolutionReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExerciseSolutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSolutionReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSolutionReviews_ExerciseSolutions_ExerciseSolutionId",
                        column: x => x.ExerciseSolutionId,
                        principalTable: "ExerciseSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseSolutionReviews_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSolutionRatings_ExerciseSolutionId",
                table: "ExerciseSolutionRatings",
                column: "ExerciseSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSolutionRatings_UserId",
                table: "ExerciseSolutionRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSolutionReviews_AuthorId",
                table: "ExerciseSolutionReviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSolutionReviews_ExerciseSolutionId",
                table: "ExerciseSolutionReviews",
                column: "ExerciseSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSolutions_AuthorId",
                table: "ExerciseSolutions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSolutions_ExerciseId",
                table: "ExerciseSolutions",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSolutionRatings");

            migrationBuilder.DropTable(
                name: "ExerciseSolutionReviews");

            migrationBuilder.DropTable(
                name: "ExerciseSolutions");
        }
    }
}
