using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "96c94f9c-3fdb-4308-be05-8819b021996b", null, "Supervisor", "SUPERVISOR" },
                    { "c8e7ba9d-a186-4af5-9baa-140f51b3575f", null, "Employee", "EMPLOYEE" },
                    { "e8bfb61c-23f4-4a03-9534-a4efdc4ab6fa", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ba873cf8-6060-41dc-9495-05fb9966e273", 0, "6aa72b34-2b46-48ab-b1ec-ca2bf9031b3b", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEHiE6wsPySjJu9zSvwwN1unkctIBOc2dgSP2Wi8Do545pHjzknNP0AB49XY2M4PPWQ==", null, false, "e68c0562-2484-4e37-b04f-f303ace0ee4f", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e8bfb61c-23f4-4a03-9534-a4efdc4ab6fa", "ba873cf8-6060-41dc-9495-05fb9966e273" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96c94f9c-3fdb-4308-be05-8819b021996b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8e7ba9d-a186-4af5-9baa-140f51b3575f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e8bfb61c-23f4-4a03-9534-a4efdc4ab6fa", "ba873cf8-6060-41dc-9495-05fb9966e273" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8bfb61c-23f4-4a03-9534-a4efdc4ab6fa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273");
        }
    }
}
