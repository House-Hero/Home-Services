using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 1, 15, 57, 27, 885, DateTimeKind.Local).AddTicks(3581),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 1, 14, 38, 46, 739, DateTimeKind.Local).AddTicks(6095));

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Requests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 1, 14, 38, 46, 739, DateTimeKind.Local).AddTicks(6095),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 1, 15, 57, 27, 885, DateTimeKind.Local).AddTicks(3581));

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Requests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
