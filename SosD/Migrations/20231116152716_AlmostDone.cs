using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SosD.Migrations
{
    /// <inheritdoc />
    public partial class AlmostDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagemPelicula",
                table: "Presupuesto",
                newName: "ImagenPrenda");

            migrationBuilder.AddColumn<int>(
                name: "DiseñoId",
                table: "PresupuestoViewModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PresupuestoViewModel_DiseñoId",
                table: "PresupuestoViewModel",
                column: "DiseñoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresupuestoViewModel_Diseño_DiseñoId",
                table: "PresupuestoViewModel",
                column: "DiseñoId",
                principalTable: "Diseño",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresupuestoViewModel_Diseño_DiseñoId",
                table: "PresupuestoViewModel");

            migrationBuilder.DropIndex(
                name: "IX_PresupuestoViewModel_DiseñoId",
                table: "PresupuestoViewModel");

            migrationBuilder.DropColumn(
                name: "DiseñoId",
                table: "PresupuestoViewModel");

            migrationBuilder.RenameColumn(
                name: "ImagenPrenda",
                table: "Presupuesto",
                newName: "ImagemPelicula");
        }
    }
}
