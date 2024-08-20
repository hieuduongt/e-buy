using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("570431fa-36ab-45bc-b135-3f6060be55e0"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "LastActiveDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f356b3f1-8f55-4a44-aac1-ea11bb54fafb", new DateTime(2024, 8, 20, 6, 27, 5, 493, DateTimeKind.Utc).AddTicks(2751), new DateTime(2024, 8, 20, 6, 27, 5, 493, DateTimeKind.Utc).AddTicks(2758), "AQAAAAIAAYagAAAAEIqvwJt/tdDtexIYR++SUA6ZQu91xrRBe1QPrGyDF0Xagzw8m4YwFlcKMg+7jstloA==", "fd9077f3-16a3-41a9-806b-d5003fb9053a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("570431fa-36ab-45bc-b135-3f6060be55e0"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "LastActiveDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5669e9c-1c42-42a2-900d-b39dbd51abdd", new DateTime(2024, 8, 19, 4, 48, 26, 89, DateTimeKind.Utc).AddTicks(7111), new DateTime(2024, 8, 19, 4, 48, 26, 89, DateTimeKind.Utc).AddTicks(7118), "AQAAAAIAAYagAAAAEG1+efvJxfbb3QsQOdgTGXwxxRlg6x8qLTOu9Q6rtKNcRwD4v2rxea7MbMzIpeX2Rw==", "821d1bcf-3468-4560-9b8e-98a7b4bbf078" });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
