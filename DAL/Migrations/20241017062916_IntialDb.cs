using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class IntialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 17, 9, 29, 14, 457, DateTimeKind.Local).AddTicks(5888),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 16, 16, 5, 35, 159, DateTimeKind.Local).AddTicks(9856));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 16, 16, 5, 35, 159, DateTimeKind.Local).AddTicks(9856),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 17, 9, 29, 14, 457, DateTimeKind.Local).AddTicks(5888));
        }
    }
}
