using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_MVC.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Palabra = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Apellido = table.Column<string>(type: "varchar(50)", nullable: false),
                    Mail = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false),
                    EsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Contenido = table.Column<string>(type: "varchar(350)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Post_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAmigo",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_Amigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAmigo", x => new { x.ID_Usuario, x.ID_Amigo });
                    table.ForeignKey(
                        name: "FK_UsuarioAmigo_Usuarios_ID_Amigo",
                        column: x => x.ID_Amigo,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioAmigo_Usuarios_ID_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Contenido = table.Column<string>(type: "varchar(350)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comentario_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentario_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reaccion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "varchar(5)", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reaccion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reaccion_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reaccion_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagPost",
                columns: table => new
                {
                    ID_Tag = table.Column<int>(type: "int", nullable: false),
                    ID_Post = table.Column<int>(type: "int", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagPost", x => new { x.ID_Tag, x.ID_Post });
                    table.ForeignKey(
                        name: "FK_TagPost_Post_ID_Post",
                        column: x => x.ID_Post,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagPost_Tag_ID_Tag",
                        column: x => x.ID_Tag,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID", "Apellido", "Bloqueado", "EsAdmin", "Mail", "Nombre", "Password" },
                values: new object[] { 1, "adminApellido", false, true, "administrador@gmail.com", "administrador", "administrador" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID", "Apellido", "Bloqueado", "EsAdmin", "Mail", "Nombre", "Password" },
                values: new object[] { 2, "usuario1Apellido", false, false, "usuario1@gmail.com", "usuario1", "usuario1" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID", "Apellido", "Bloqueado", "EsAdmin", "Mail", "Nombre", "Password" },
                values: new object[] { 3, "usuario2Apellido", false, false, "usuario2@gmail.com", "usuario2", "usuario2" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "ID", "Contenido", "Fecha", "UsuarioID" },
                values: new object[,]
                {
                    { 1, "111", new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430), 2 },
                    { 2, "222", new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430), 3 },
                    { 3, "333", new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430), 2 },
                    { 4, "444", new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430), 3 },
                    { 5, "555", new DateTime(2022, 7, 18, 19, 34, 12, 310, DateTimeKind.Local).AddTicks(3430), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_PostID",
                table: "Comentario",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_UsuarioID",
                table: "Comentario",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UsuarioID",
                table: "Post",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Reaccion_PostID",
                table: "Reaccion",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Reaccion_UsuarioID",
                table: "Reaccion",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_TagPost_ID_Post",
                table: "TagPost",
                column: "ID_Post");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAmigo_ID_Amigo",
                table: "UsuarioAmigo",
                column: "ID_Amigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Reaccion");

            migrationBuilder.DropTable(
                name: "TagPost");

            migrationBuilder.DropTable(
                name: "UsuarioAmigo");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
