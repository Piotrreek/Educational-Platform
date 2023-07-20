using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDeleteAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DidacticMaterialOpinions_Users_AuthorId",
                table: "DidacticMaterialOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_DidacticMaterials_Users_AuthorId",
                table: "DidacticMaterials");

            migrationBuilder.AddForeignKey(
                name: "FK_DidacticMaterialOpinions_Users_AuthorId",
                table: "DidacticMaterialOpinions",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DidacticMaterials_Users_AuthorId",
                table: "DidacticMaterials",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DidacticMaterialOpinions_Users_AuthorId",
                table: "DidacticMaterialOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_DidacticMaterials_Users_AuthorId",
                table: "DidacticMaterials");

            migrationBuilder.AddForeignKey(
                name: "FK_DidacticMaterialOpinions_Users_AuthorId",
                table: "DidacticMaterialOpinions",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DidacticMaterials_Users_AuthorId",
                table: "DidacticMaterials",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
