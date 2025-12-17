using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExtraLeaveRequests2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d2fd2c9-2e0f-4aa2-8c71-abd40fdcac62", "AQAAAAIAAYagAAAAEEtjSSSky/jUBqk5qWscmpl8QMCs2KpCmZgs2/4P0J7xw6afhEm+kkaT6daAgFYPlA==", "330e119e-121b-4da6-80ce-f975186a0600" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b51001b-0d17-42ef-baeb-23ce5991a18e", "AQAAAAIAAYagAAAAEO8GcNfbicX5jX80HAe6tJA1WP+4uisjGE6PefY0V0YbgE4IBcGjqXTtDf2t1L408g==", "29249caf-790d-43d1-8482-0fdad3a90f91" });
        }
    }
}
