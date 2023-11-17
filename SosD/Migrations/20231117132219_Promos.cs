using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SosD.Migrations
{
    /// <inheritdoc />
    public partial class Promos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedioPago",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedioPago", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CantMin = table.Column<int>(type: "int", nullable: true),
                    Descuento = table.Column<int>(type: "int", nullable: true),
                    MedioPagoId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Promociones_MedioPago_MedioPagoId",
                        column: x => x.MedioPagoId,
                        principalTable: "MedioPago",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_MedioPagoId",
                table: "Promociones",
                column: "MedioPagoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promociones");

            migrationBuilder.DropTable(
                name: "MedioPago");
        }
    }
}
