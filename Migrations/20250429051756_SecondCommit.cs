using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SHAKA.RestApi.EF.Example.API.Migrations
{
    /// <inheritdoc />
    public partial class SecondCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MyUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MyUsers",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MyUsers",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsActive", "LastName", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 29, 5, 11, 50, 48, DateTimeKind.Utc).AddTicks(3941), "john.doe@example.com", "John", true, "Doe", "johndoe" },
                    { 2, new DateTime(2025, 4, 29, 5, 11, 50, 48, DateTimeKind.Utc).AddTicks(4237), "jane.doe@example.com", "Jane", true, "Doe", "janedoe" }
                });
        }
    }
}
