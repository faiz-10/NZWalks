using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdataforregionsanddifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c7b18847-5268-444b-9283-d8fd47bf2486"), "Medium" },
                    { new Guid("e6c626ae-c6db-4787-a374-2b4eeb36a234"), "Easy" },
                    { new Guid("fcf83289-488c-4d2c-8e04-9d63755f5d8d"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("254749c3-5f7f-488e-a330-8025f483238c"), "NSN", "Nelson", null },
                    { new Guid("2f05301e-009a-403d-827a-68c5f4c63c32"), "STL", "Southland", "https://images.pexels.com/photos/1353248/pexels-photo-1353248.jpeg" },
                    { new Guid("3e0f0ab4-19b2-46a6-b074-c48a359d4654"), "BOP", "Bay of Plenty", null },
                    { new Guid("880eba07-6f2d-4e6c-baff-a42255df86c8"), "NTL", "Northland", "https://images.pexels.com/photos/12348118/pexels-photo-12348118.jpeg" },
                    { new Guid("8bd0927d-bf7e-4cfc-800b-0ab8260b97b1"), "AKL", "Auckland", "https://images.pexels.com/photos/29724794/pexels-photo-29724794.jpeg" },
                    { new Guid("ce4b2919-a415-48e4-8f4e-99e332f863d9"), "WGN", "Wellington", "https://images.pexels.com/photos/32540627/pexels-photo-32540627.jpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c7b18847-5268-444b-9283-d8fd47bf2486"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e6c626ae-c6db-4787-a374-2b4eeb36a234"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("fcf83289-488c-4d2c-8e04-9d63755f5d8d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("254749c3-5f7f-488e-a330-8025f483238c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2f05301e-009a-403d-827a-68c5f4c63c32"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3e0f0ab4-19b2-46a6-b074-c48a359d4654"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("880eba07-6f2d-4e6c-baff-a42255df86c8"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8bd0927d-bf7e-4cfc-800b-0ab8260b97b1"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ce4b2919-a415-48e4-8f4e-99e332f863d9"));
        }
    }
}
