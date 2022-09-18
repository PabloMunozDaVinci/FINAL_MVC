using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_MVC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Intentos",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2022, 9, 17, 20, 49, 32, 655, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2022, 9, 17, 20, 49, 32, 655, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2022, 9, 17, 20, 49, 32, 655, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2022, 9, 17, 20, 49, 32, 655, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2022, 9, 17, 20, 49, 32, 655, DateTimeKind.Local).AddTicks(2847));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intentos",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "ID",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430));
        }
    }
}
