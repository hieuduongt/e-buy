using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addlikestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("570431fa-36ab-45bc-b135-3f6060be55e0"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "LastActiveDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5669e9c-1c42-42a2-900d-b39dbd51abdd", new DateTime(2024, 8, 19, 4, 48, 26, 89, DateTimeKind.Utc).AddTicks(7111), new DateTime(2024, 8, 19, 4, 48, 26, 89, DateTimeKind.Utc).AddTicks(7118), "AQAAAAIAAYagAAAAEG1+efvJxfbb3QsQOdgTGXwxxRlg6x8qLTOu9Q6rtKNcRwD4v2rxea7MbMzIpeX2Rw==", "821d1bcf-3468-4560-9b8e-98a7b4bbf078" });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_productId",
                table: "Likes",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("570431fa-36ab-45bc-b135-3f6060be55e0"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "LastActiveDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6ab12d1-680f-4e8b-9f60-58777fd86d73", new DateTime(2024, 8, 17, 6, 5, 32, 403, DateTimeKind.Utc).AddTicks(5804), new DateTime(2024, 8, 17, 6, 5, 32, 403, DateTimeKind.Utc).AddTicks(5809), "AQAAAAIAAYagAAAAEHUjJAyykQLmnJ0P/9RbnqCcqKuA9ZS5msD4DtIQnjCIh0jZhMsn0T0Gq88iddMkdg==", "3504234a-e833-4124-a09d-eba508fc48fa" });
        }
    }
}
