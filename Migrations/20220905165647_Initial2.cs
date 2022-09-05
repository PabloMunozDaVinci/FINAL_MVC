using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_MVC.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 56, 46, 556, DateTimeKind.Local).AddTicks(8698));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 56, 46, 556, DateTimeKind.Local).AddTicks(8698));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 56, 46, 556, DateTimeKind.Local).AddTicks(8698));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 56, 46, 556, DateTimeKind.Local).AddTicks(8698));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 56, 46, 556, DateTimeKind.Local).AddTicks(8698));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 53, 5, 836, DateTimeKind.Local).AddTicks(6137));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 53, 5, 836, DateTimeKind.Local).AddTicks(6137));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 53, 5, 836, DateTimeKind.Local).AddTicks(6137));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 53, 5, 836, DateTimeKind.Local).AddTicks(6137));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2022, 9, 5, 13, 53, 5, 836, DateTimeKind.Local).AddTicks(6137));
        }
    }
}
