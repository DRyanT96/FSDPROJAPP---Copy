using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSDPROJAPP.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultDataAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad2bcf0c-20db-474f-8407-5a6b159518ba", null, "Administrator", "ADMINISTRATOR" },
                    { "bd2bcf0c-20db-474f-8407-5a6b159518bb", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3781efa7-66dc-47f0-860f-e506d04102e4", 0, "784c99b6-c3f7-4440-b4bf-e1e2b8cd5060", "admin@localhost.com", false, "Admin", "User", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEKGHkGoIO0D3y85+aYvIdUd7FlXcZ+IxwPpUzd1KDFD9byyUP6Us0CUnXpK4bxY+bw==", null, false, "961d1dce-0bda-472a-ade0-3101408f725c", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6904), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6906), "Detail-1", "System" },
                    { 2, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6908), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6908), "Detail-2", "System" }
                });

            migrationBuilder.InsertData(
                table: "Dislikes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7319), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7320), "Matcha", "System" },
                    { 2, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7321), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7322), "Shopping", "System" }
                });

            migrationBuilder.InsertData(
                table: "Hobbys",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6539), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6556), "Soccer", "System" },
                    { 2, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6558), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(6559), "Basketball", "System" }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7110), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7111), "The Color Blue", "System" },
                    { 2, "System", new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7112), new DateTime(2024, 1, 24, 20, 27, 9, 412, DateTimeKind.Local).AddTicks(7112), "Rich Guys", "System" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ad2bcf0c-20db-474f-8407-5a6b159518ba", "3781efa7-66dc-47f0-860f-e506d04102e4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd2bcf0c-20db-474f-8407-5a6b159518bb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad2bcf0c-20db-474f-8407-5a6b159518ba", "3781efa7-66dc-47f0-860f-e506d04102e4" });

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dislikes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dislikes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hobbys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hobbys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad2bcf0c-20db-474f-8407-5a6b159518ba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3781efa7-66dc-47f0-860f-e506d04102e4");
        }
    }
}
