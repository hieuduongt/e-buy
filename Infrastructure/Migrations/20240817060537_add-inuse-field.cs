using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addinusefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InUse",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("570431fa-36ab-45bc-b135-3f6060be55e0"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "LastActiveDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6ab12d1-680f-4e8b-9f60-58777fd86d73", new DateTime(2024, 8, 17, 6, 5, 32, 403, DateTimeKind.Utc).AddTicks(5804), new DateTime(2024, 8, 17, 6, 5, 32, 403, DateTimeKind.Utc).AddTicks(5809), "AQAAAAIAAYagAAAAEHUjJAyykQLmnJ0P/9RbnqCcqKuA9ZS5msD4DtIQnjCIh0jZhMsn0T0Gq88iddMkdg==", "3504234a-e833-4124-a09d-eba508fc48fa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InUse",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("570431fa-36ab-45bc-b135-3f6060be55e0"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "LastActiveDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6de3773-40c1-4ae4-b8c8-ad25552dd783", new DateTime(2024, 7, 25, 16, 12, 36, 387, DateTimeKind.Utc).AddTicks(4053), new DateTime(2024, 7, 25, 16, 12, 36, 387, DateTimeKind.Utc).AddTicks(4059), "AQAAAAIAAYagAAAAEIb39dOzph3L+rCmJV6vhxeqX2WwFuF2YqPAZr8WJz/ko6Zo0kvR4fBr+Nw3QhNKhw==", "be04b151-0055-4d64-b0af-2327fbbbbb08" });
        }
    }
}
