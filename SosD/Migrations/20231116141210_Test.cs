using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SosD.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseño",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseño", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo Tela",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo Tela", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo Prenda",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    TipoTelaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo Prenda", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tipo Prenda_Tipo Tela_TipoTelaId",
                        column: x => x.TipoTelaId,
                        principalTable: "Tipo Tela",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PresupuestoViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    TipoPrendaId = table.Column<int>(type: "int", nullable: true),
                    PrecioUni = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImagenPrenda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresupuestoViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PresupuestoViewModel_Tipo Prenda_TipoPrendaId",
                        column: x => x.TipoPrendaId,
                        principalTable: "Tipo Prenda",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PresupuestoViewModel_TipoPrendaId",
                table: "PresupuestoViewModel",
                column: "TipoPrendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tipo Prenda_TipoTelaId",
                table: "Tipo Prenda",
                column: "TipoTelaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diseño");

            migrationBuilder.DropTable(
                name: "PresupuestoViewModel");

            migrationBuilder.DropTable(
                name: "Tipo Prenda");

            migrationBuilder.DropTable(
                name: "Tipo Tela");
        }
    }
}
