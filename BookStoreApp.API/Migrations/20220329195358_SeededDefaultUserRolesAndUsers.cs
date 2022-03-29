using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    public partial class SeededDefaultUserRolesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e0ba207-38d3-44de-ae69-712cc644d60b", "23c7d527-3035-4c47-8344-5e37a018e0d0", "Administrator", "ADMINISTRATOR" },
                    { "ff350eec-aeb0-4d9b-a063-cc4c9d4e23f0", "1ba34208-d044-45ef-9ff0-fc252680623c", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7f2d133d-097b-4307-bff4-e09c21a4f552", 0, "531d9fc7-afbf-4cf1-931f-830420c1b5c2", "here@there.com", false, "Larry", "Bellou", false, null, null, "HERE@THERE.COM", "AQAAAAEAACcQAAAAEEE+yE4DKrqD1O22DiNRCVkWtrEwi1TdeMx3l2cZAg/rTtXpvdBMjdA0AOS8MEyLKw==", null, false, "946b849b-f8a8-46e3-a33f-c14e341ede7e", false, "here@there.com" },
                    { "895dc575-444f-4827-884e-d6c28b77a46d", 0, "993a420d-339c-4c9d-9429-aed0f345c6c2", "Colvert@there.com", false, "Viviann", "Colvert", false, null, null, "COLVERT@THERE.COM", "AQAAAAEAACcQAAAAEINzrqEuZOyrhGZ2ykxjrBTfd7fvsQBowoR0vkDdog6DQk3GWQENRs/Y/Y5CEMTBlA==", null, false, "67d30a0c-a793-4d0e-81fe-3bc0a86fbbfb", false, "Colvert@there.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0e0ba207-38d3-44de-ae69-712cc644d60b", "7f2d133d-097b-4307-bff4-e09c21a4f552" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ff350eec-aeb0-4d9b-a063-cc4c9d4e23f0", "895dc575-444f-4827-884e-d6c28b77a46d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0e0ba207-38d3-44de-ae69-712cc644d60b", "7f2d133d-097b-4307-bff4-e09c21a4f552" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ff350eec-aeb0-4d9b-a063-cc4c9d4e23f0", "895dc575-444f-4827-884e-d6c28b77a46d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e0ba207-38d3-44de-ae69-712cc644d60b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff350eec-aeb0-4d9b-a063-cc4c9d4e23f0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f2d133d-097b-4307-bff4-e09c21a4f552");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "895dc575-444f-4827-884e-d6c28b77a46d");
        }
    }
}
