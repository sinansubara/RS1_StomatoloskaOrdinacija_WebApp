using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class ispravakuspostavljenedijagnozeikartona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinskiKarton_Terapija_TerapijaId",
                table: "MedicinskiKarton");

            migrationBuilder.DropIndex(
                name: "IX_MedicinskiKarton_TerapijaId",
                table: "MedicinskiKarton");

            migrationBuilder.DropColumn(
                name: "TerapijaId",
                table: "MedicinskiKarton");

            migrationBuilder.AddColumn<int>(
                name: "UspostavljenaDijagnozaId",
                table: "Pregledi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pregledi_UspostavljenaDijagnozaId",
                table: "Pregledi",
                column: "UspostavljenaDijagnozaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregledi_UspostavljenaDijagnoza_UspostavljenaDijagnozaId",
                table: "Pregledi",
                column: "UspostavljenaDijagnozaId",
                principalTable: "UspostavljenaDijagnoza",
                principalColumn: "UspostavljenaDijagnozaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregledi_UspostavljenaDijagnoza_UspostavljenaDijagnozaId",
                table: "Pregledi");

            migrationBuilder.DropIndex(
                name: "IX_Pregledi_UspostavljenaDijagnozaId",
                table: "Pregledi");

            migrationBuilder.DropColumn(
                name: "UspostavljenaDijagnozaId",
                table: "Pregledi");

            migrationBuilder.AddColumn<int>(
                name: "TerapijaId",
                table: "MedicinskiKarton",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskiKarton_TerapijaId",
                table: "MedicinskiKarton",
                column: "TerapijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinskiKarton_Terapija_TerapijaId",
                table: "MedicinskiKarton",
                column: "TerapijaId",
                principalTable: "Terapija",
                principalColumn: "TerapijaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
