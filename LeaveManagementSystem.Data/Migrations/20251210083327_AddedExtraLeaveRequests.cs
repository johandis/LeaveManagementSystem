using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExtraLeaveRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b51001b-0d17-42ef-baeb-23ce5991a18e", "AQAAAAIAAYagAAAAEO8GcNfbicX5jX80HAe6tJA1WP+4uisjGE6PefY0V0YbgE4IBcGjqXTtDf2t1L408g==", "29249caf-790d-43d1-8482-0fdad3a90f91" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67f32412-3243-4176-8c97-dca473cff212", "AQAAAAIAAYagAAAAEDhJSuCP+0h35GoH9fIBbyUyB9ZvbpoH88Xq8Sw9zAGeM1LN9qa1TDTdj03cGnwe/w==", "b62d6fb3-f105-4536-baa9-44a8c01d748f" });
        }
    }
}
