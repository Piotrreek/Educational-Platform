using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreateAcademyEntityRequest_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreateAcademyEntityRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversitySubjectDegree = table.Column<int>(type: "int", nullable: true),
                    UniversityCourseSession = table.Column<int>(type: "int", nullable: true),
                    UniversityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UniversitySubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateAcademyEntityRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreateAcademyEntityRequests_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreateAcademyEntityRequests_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreateAcademyEntityRequests_UniversitySubjects_UniversitySubjectId",
                        column: x => x.UniversitySubjectId,
                        principalTable: "UniversitySubjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreateAcademyEntityRequests_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreateAcademyEntityRequests_FacultyId",
                table: "CreateAcademyEntityRequests",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateAcademyEntityRequests_RequesterId",
                table: "CreateAcademyEntityRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateAcademyEntityRequests_UniversityId",
                table: "CreateAcademyEntityRequests",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateAcademyEntityRequests_UniversitySubjectId",
                table: "CreateAcademyEntityRequests",
                column: "UniversitySubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateAcademyEntityRequests");
        }
    }
}
