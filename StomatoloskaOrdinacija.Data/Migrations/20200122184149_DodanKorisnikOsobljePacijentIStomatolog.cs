using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class DodanKorisnikOsobljePacijentIStomatolog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(maxLength: 100, nullable: false),
                    Prezime = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    JMBG = table.Column<string>(maxLength: 13, nullable: false),
                    Mobitel = table.Column<string>(maxLength: 30, nullable: false),
                    Adresa = table.Column<string>(maxLength: 200, nullable: false),
                    KorisnickoIme = table.Column<string>(maxLength: 100, nullable: false),
                    Lozinka = table.Column<string>(maxLength: 100, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Titula = table.Column<string>(maxLength: 100, nullable: true),
                    DatumZaposlenja = table.Column<DateTime>(nullable: true),
                    DatumRodjenja = table.Column<DateTime>(nullable: true),
                    Stomatolog_Titula = table.Column<string>(maxLength: 100, nullable: true),
                    Stomatolog_DatumZaposlenja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
