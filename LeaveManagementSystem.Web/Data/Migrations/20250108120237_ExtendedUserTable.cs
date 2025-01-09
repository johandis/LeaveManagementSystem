using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b4ad75c-9216-4351-b350-2ffeac44e5dc", new DateOnly(1950, 12, 1), "Default", "Admin", "AQAAAAIAAYagAAAAEBx2OWd3fmiq11lbW+PmDCAnXaf2IL5x3aYzs7AmYteqBwJfoBvg+HL204fKgC1XKQ==", "7db36c10-4af5-45f8-8c63-9b72338409c5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6aa72b34-2b46-48ab-b1ec-ca2bf9031b3b", "AQAAAAIAAYagAAAAEHiE6wsPySjJu9zSvwwN1unkctIBOc2dgSP2Wi8Do545pHjzknNP0AB49XY2M4PPWQ==", "e68c0562-2484-4e37-b04f-f303ace0ee4f" });
        }
    }
}
