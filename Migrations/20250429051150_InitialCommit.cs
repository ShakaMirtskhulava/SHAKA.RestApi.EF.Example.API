using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SHAKA.RestApi.EF.Example.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MyUsers",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsActive", "LastName", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 29, 5, 11, 50, 48, DateTimeKind.Utc).AddTicks(3941), "john.doe@example.com", "John", true, "Doe", "johndoe" },
                    { 2, new DateTime(2025, 4, 29, 5, 11, 50, 48, DateTimeKind.Utc).AddTicks(4237), "jane.doe@example.com", "Jane", true, "Doe", "janedoe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyUsers");
        }
    }
}
