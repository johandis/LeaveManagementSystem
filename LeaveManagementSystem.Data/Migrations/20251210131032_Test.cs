using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad60f0f7-c9f1-4fde-9f49-5944a855c2b0", "AQAAAAIAAYagAAAAEFDmjBDk9cpX4gpomeWC7OnQb2dxthIbqLOnbHTrQYiujHLZ/v0YC8Mo3lPICn8cIw==", "52c979e4-3259-42de-b7e5-75c5760e3661" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "680865ff-d318-4df2-b9ce-7315e57abf78", "AQAAAAIAAYagAAAAEJm6nrOKxFI2T3JNTYbJyvllHoIyMnJS/bVDfNYAN2Gtz8f4TvYcTVvdcIQDRUkkvQ==", "db512aef-e675-437b-ad01-d93504b00c63" });
        }
    }
}
