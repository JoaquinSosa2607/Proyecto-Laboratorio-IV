using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SosD.Migrations
{
    /// <inheritdoc />
    public partial class AAAAAAAAAAA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Tipo Tela_TipoTelaId",
                table: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_TipoTelaId",
                table: "Presupuesto");

            migrationBuilder.DropColumn(
                name: "TipoTelaId",
                table: "Presupuesto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoTelaId",
                table: "Presupuesto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_TipoTelaId",
                table: "Presupuesto",
                column: "TipoTelaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_Tipo Tela_TipoTelaId",
                table: "Presupuesto",
                column: "TipoTelaId",
                principalTable: "Tipo Tela",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
