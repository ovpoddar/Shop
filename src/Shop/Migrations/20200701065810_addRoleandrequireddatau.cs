using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class addRoleandrequireddatau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3cfd559-5325-4507-b6a3-f8711256f868");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77971948-55cb-4adc-899d-965fab136782");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "456110a4-27ab-45a7-a0ce-5c5571aad34c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastLogin", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, true, "Home", "d49ec2b3-b652-441d-b25a-e512a7fff4af", "admin@gmail.com", false, "Shop", "Male", new DateTime(2020, 7, 1, 12, 28, 10, 167, DateTimeKind.Local).AddTicks(7113), "Keeper", true, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAENy1MasO3pTdOMRvDS7vQ6H0hs5NL9hOIgMbPIvj8WeSPwHB4D3C7BlwU6QzJ+JHCA==", "8436159825", false, "7c0c44ae-8a67-4840-ace4-1e5749ed4fab", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Balances",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 7, 1, 12, 28, 10, 165, DateTimeKind.Local).AddTicks(7805));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3cfd559-5325-4507-b6a3-f8711256f868", "1b660393-8235-4257-a7f6-d90ee2efd1c2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastLogin", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "77971948-55cb-4adc-899d-965fab136782", 0, true, "Home", "6fca286c-3bf5-4601-967b-0ab57159c491", "admin@gmail.com", false, "Shop", "Male", new DateTime(2020, 7, 1, 12, 23, 8, 265, DateTimeKind.Local).AddTicks(1462), "Keeper", true, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAENy1MasO3pTdOMRvDS7vQ6H0hs5NL9hOIgMbPIvj8WeSPwHB4D3C7BlwU6QzJ+JHCA==", "8436159825", false, "aa32abe5-77ee-439d-b1b5-0595d3b47cd5", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Balances",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 7, 1, 12, 23, 8, 263, DateTimeKind.Local).AddTicks(2322));
        }
    }
}
