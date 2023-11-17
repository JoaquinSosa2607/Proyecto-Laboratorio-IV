using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SosD.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presupuesto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    TipoPrendaId = table.Column<int>(type: "int", nullable: true),
                    ImagemPelicula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoTelaId = table.Column<int>(type: "int", nullable: true),
                    DiseñoId = table.Column<int>(type: "int", nullable: true),
                    PrecioUni = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuesto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Diseño_DiseñoId",
                        column: x => x.DiseñoId,
                        principalTable: "Diseño",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Tipo Prenda_TipoPrendaId",
                        column: x => x.TipoPrendaId,
                        principalTable: "Tipo Prenda",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Tipo Tela_TipoTelaId",
                        column: x => x.TipoTelaId,
                        principalTable: "Tipo Tela",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_DiseñoId",
                table: "Presupuesto",
                column: "DiseñoId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_TipoPrendaId",
                table: "Presupuesto",
                column: "TipoPrendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_TipoTelaId",
                table: "Presupuesto",
                column: "TipoTelaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presupuesto");
        }
    }
}
