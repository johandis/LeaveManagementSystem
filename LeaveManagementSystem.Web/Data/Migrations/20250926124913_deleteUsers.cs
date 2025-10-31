using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class deleteUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0168f42-ff38-4884-a4de-17849891ea71", "AQAAAAIAAYagAAAAEEc6jqoJKo+T9mBhXiJVwl7sx7zDxPVnT+wZZo6JZPg4qVkRqbE2SDGhvQXrqQjujw==", "aacbd623-8720-4489-875f-3b711b67076b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba873cf8-6060-41dc-9495-05fb9966e273",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56cf23bf-d867-44af-9f5c-5e750657db4a", "AQAAAAIAAYagAAAAEN3b6ObH6XcOYt0lLQ84CF2n3qAgphdgypghMnADqJzgDVzzwdMFUVKEWc0RQm84SQ==", "a8e683be-1815-411a-ab1a-00d66c7247f6" });
        }
    }
}
