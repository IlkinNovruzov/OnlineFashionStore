using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFashionStore.Migrations
{
    /// <inheritdoc />
    public partial class migColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Colors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b37ac15b-b3ca-49d1-b719-de26fc2958ef", "AQAAAAIAAYagAAAAEIAWfX69AXP6pjxQxvSowwzw40bXEoqfNUfBv+3LV21766JcSMq4yyrnePAKUw9dWA==", "aa4ec840-09ef-4fdf-990b-769d24387d12" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Colors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6e68d02-582a-43cc-996f-a6260b37d657", "AQAAAAIAAYagAAAAEJyTc7wMOQNfj41nPC3LTOkfCi7yKLSj/RUohpmf3BTkjJl7UxfGqmKkjy1vKIF0bQ==", "f2012299-39b9-40a8-965c-9a074b86d54d" });
        }
    }
}
