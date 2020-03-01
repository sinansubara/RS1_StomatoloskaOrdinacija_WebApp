using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class StavkeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Racun_RacunStavkeId",
                table: "Racun");

            migrationBuilder.AddColumn<int>(
                name: "UlazUSkladisteId",
                table: "UlazStavke",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RacunId",
                table: "RacunStavke",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UlazStavke_UlazUSkladisteId",
                table: "UlazStavke",
                column: "UlazUSkladisteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RacunStavke_RacunId",
                table: "RacunStavke",
                column: "RacunId");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_RacunStavkeId",
                table: "Racun",
                column: "RacunStavkeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RacunStavke_Racun_RacunId",
                table: "RacunStavke",
                column: "RacunId",
                principalTable: "Racun",
                principalColumn: "RacunId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UlazStavke_UlazUSkladiste_UlazUSkladisteId",
                table: "UlazStavke",
                column: "UlazUSkladisteId",
                principalTable: "UlazUSkladiste",
                principalColumn: "UlazUSkladisteID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RacunStavke_Racun_RacunId",
                table: "RacunStavke");

            migrationBuilder.DropForeignKey(
                name: "FK_UlazStavke_UlazUSkladiste_UlazUSkladisteId",
                table: "UlazStavke");

            migrationBuilder.DropIndex(
                name: "IX_UlazStavke_UlazUSkladisteId",
                table: "UlazStavke");

            migrationBuilder.DropIndex(
                name: "IX_RacunStavke_RacunId",
                table: "RacunStavke");

            migrationBuilder.DropIndex(
                name: "IX_Racun_RacunStavkeId",
                table: "Racun");

            migrationBuilder.DropColumn(
                name: "UlazUSkladisteId",
                table: "UlazStavke");

            migrationBuilder.DropColumn(
                name: "RacunId",
                table: "RacunStavke");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_RacunStavkeId",
                table: "Racun",
                column: "RacunStavkeId");
        }
    }
}
