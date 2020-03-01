using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class fixRacunIUlazuSkladiste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racun_RacunStavke_RacunStavkeId",
                table: "Racun");

            migrationBuilder.DropForeignKey(
                name: "FK_UlazUSkladiste_UlazStavke_UlazStavkeId",
                table: "UlazUSkladiste");

            migrationBuilder.DropIndex(
                name: "IX_UlazUSkladiste_UlazStavkeId",
                table: "UlazUSkladiste");

            migrationBuilder.DropIndex(
                name: "IX_UlazStavke_UlazUSkladisteId",
                table: "UlazStavke");

            migrationBuilder.DropIndex(
                name: "IX_Racun_RacunStavkeId",
                table: "Racun");

            migrationBuilder.DropColumn(
                name: "UlazStavkeId",
                table: "UlazUSkladiste");

            migrationBuilder.DropColumn(
                name: "RacunStavkeId",
                table: "Racun");

            migrationBuilder.CreateIndex(
                name: "IX_UlazStavke_UlazUSkladisteId",
                table: "UlazStavke",
                column: "UlazUSkladisteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UlazStavke_UlazUSkladisteId",
                table: "UlazStavke");

            migrationBuilder.AddColumn<int>(
                name: "UlazStavkeId",
                table: "UlazUSkladiste",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RacunStavkeId",
                table: "Racun",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UlazUSkladiste_UlazStavkeId",
                table: "UlazUSkladiste",
                column: "UlazStavkeId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazStavke_UlazUSkladisteId",
                table: "UlazStavke",
                column: "UlazUSkladisteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Racun_RacunStavkeId",
                table: "Racun",
                column: "RacunStavkeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Racun_RacunStavke_RacunStavkeId",
                table: "Racun",
                column: "RacunStavkeId",
                principalTable: "RacunStavke",
                principalColumn: "RacunStavkeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UlazUSkladiste_UlazStavke_UlazStavkeId",
                table: "UlazUSkladiste",
                column: "UlazStavkeId",
                principalTable: "UlazStavke",
                principalColumn: "UlazStavkeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
