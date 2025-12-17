using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixLeaveRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "680865ff-d318-4df2-b9ce-7315e57abf78", "AQAAAAIAAYagAAAAEJm6nrOKxFI2T3JNTYbJyvllHoIyMnJS/bVDfNYAN2Gtz8f4TvYcTVvdcIQDRUkkvQ==", "db512aef-e675-437b-ad01-d93504b00c63" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d2fd2c9-2e0f-4aa2-8c71-abd40fdcac62", "AQAAAAIAAYagAAAAEEtjSSSky/jUBqk5qWscmpl8QMCs2KpCmZgs2/4P0J7xw6afhEm+kkaT6daAgFYPlA==", "330e119e-121b-4da6-80ce-f975186a0600" });
        }
    }
}
