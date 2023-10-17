using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetroRecords_RecordAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Artist",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.UpdateData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4662));

            migrationBuilder.UpdateData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "Artist", "CreatedAt", "Genre", "Label", "Name", "ReleaseDate", "RunTime", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, "Prince", new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4715), "Funk Pop", "Warner Bros.", "Purple Rain", new DateTime(1984, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 43, 55, 0), null },
                    { 4, "Genesis", new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4717), "Prog Rock", "Charisma", "Selling England By The Pound", new DateTime(1973, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 53, 44, 0), null },
                    { 5, "Supertramp", new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4720), "Pop", "A&M", "Breakfast In America", new DateTime(1979, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 46, 6, 0), null },
                    { 6, "Opeth", new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4722), "Metal", "Candlelight", "My Arms, Your Hearse", new DateTime(1998, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 52, 34, 0), null },
                    { 7, "Kraftwerk", new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4725), "Electronica", "Kling Klang", "Computer World", new DateTime(1981, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 34, 25, 0), null },
                    { 8, "Metallica", new DateTime(2023, 10, 17, 15, 29, 56, 822, DateTimeKind.Local).AddTicks(4727), "Metal", "Elektra", "Master of Puppets", new DateTime(1986, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 54, 52, 0), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Records",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Artist",
                table: "Records",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 16, 12, 24, 16, 359, DateTimeKind.Local).AddTicks(2498));

            migrationBuilder.UpdateData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 16, 12, 24, 16, 359, DateTimeKind.Local).AddTicks(2544));
        }
    }
}
