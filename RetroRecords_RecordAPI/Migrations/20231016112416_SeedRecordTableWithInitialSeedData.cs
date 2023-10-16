using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetroRecords_RecordAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedRecordTableWithInitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Records");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Records",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Records",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Records",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "Artist", "CreatedAt", "Genre", "Label", "Name", "ReleaseDate", "RunTime", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "David Bowie", new DateTime(2023, 10, 16, 12, 24, 16, 359, DateTimeKind.Local).AddTicks(2498), "Glam Rock", "RCA", "Aladdin Sane", new DateTime(1973, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 41, 32, 0), null },
                    { 2, "David Bowie", new DateTime(2023, 10, 16, 12, 24, 16, 359, DateTimeKind.Local).AddTicks(2544), "Rock", "RCA", "Station To Station", new DateTime(1976, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 37, 54, 0), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Records");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Records",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
