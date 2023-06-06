using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("4b92b773-48df-4b5a-8ff5-eeaf7793e5ec"), DateTimeOffset.Now, DateTimeOffset.Now, "User" },
                    { new Guid("bc247763-b91e-42ed-ba30-e85e67bb7dd5"), DateTimeOffset.Now, DateTimeOffset.Now, "Administrator" },
                    { new Guid("d50535d9-c28a-4f5e-a6ba-e0fde250be07"), DateTimeOffset.Now, DateTimeOffset.Now, "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4b92b773-48df-4b5a-8ff5-eeaf7793e5ec"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc247763-b91e-42ed-ba30-e85e67bb7dd5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d50535d9-c28a-4f5e-a6ba-e0fde250be07"));
        }
    }
}
