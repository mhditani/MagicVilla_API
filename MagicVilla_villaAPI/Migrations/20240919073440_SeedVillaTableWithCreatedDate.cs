using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_villaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTableWithCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 9, 19, 10, 34, 40, 777, DateTimeKind.Local).AddTicks(1215), "Royal Villa provides beachfront accommodations in Trabzon. This villa offers free private parking and a shared kitchen.", "https://limestays.com/wp-content/uploads/2023/01/34-926x618.jpg", "Royal Villa", 5, 200.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2024, 9, 19, 10, 34, 40, 777, DateTimeKind.Local).AddTicks(1271), "Explore your family getaway with our premium pool villa, featuring a spacious room with two king-sized beds and a private pool right in your room.", "https://image-tc.galaxy.tf/wijpeg-vh7o2qzd9xv299ah5v1vav87/grand-lexis-port-dickson-premium-pool-villa-3-thumbnail-w4896_wide.jpg?crop=0%2C105%2C2000%2C1125&width=1140", "Premium Pool Villa", 4, 300.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2024, 9, 19, 10, 34, 40, 777, DateTimeKind.Local).AddTicks(1274), "Featuring a garden, Highend Private Pool Villa features accommodations in Amphoe Koh Samui. This beachfront property offers access to a balcony.", "https://www.luva-villas.com/img/news_11_crop_0_1671810979.webp", "Luxury Pool Villa", 4, 400.0, 750, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "", new DateTime(2024, 9, 19, 10, 34, 40, 777, DateTimeKind.Local).AddTicks(1277), "Diamond Villa has view of islands and the Aegean Sea, with private pool. The villa offers accommodation with butler service, maximized luxury and advantages.", "https://static.wixstatic.com/media/b32baf_ff2d9aeb3aff4cb99694364312052495~mv2.jpg/v1/fill/w_640,h_426,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/b32baf_ff2d9aeb3aff4cb99694364312052495~mv2.jpg", "Diamond Villa", 4, 550.0, 900, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "", new DateTime(2024, 9, 19, 10, 34, 40, 777, DateTimeKind.Local).AddTicks(1280), "Providing an outdoor swimming pool, Diamond Pool Villa@Samui provides accommodations in Koh Samui. This chalet features free private parking, free shuttle.", "https://cdn-5d68e683f911c80950255463.closte.com/wp-content/uploads/2019/05/Edit_Overview_Villa-2.jpg", "Diamond Pool Villa", 4, 600.0, 1100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
