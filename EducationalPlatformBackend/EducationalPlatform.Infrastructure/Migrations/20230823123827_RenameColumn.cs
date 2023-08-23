using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "CreateAcademyEntityRequests",
                newName: "EntityTypeName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityTypeName",
                table: "CreateAcademyEntityRequests",
                newName: "EntityType");
        }
    }
}
