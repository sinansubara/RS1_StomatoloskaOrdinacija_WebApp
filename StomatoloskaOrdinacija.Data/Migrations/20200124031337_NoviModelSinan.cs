using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class NoviModelSinan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinskoOsoblje_Korisnici_KorisnikId",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacijenti_Korisnici_KorisnikId",
                table: "Pacijenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Stomatolozi_Korisnici_KorisnikId",
                table: "Stomatolozi");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Stomatolozi_KorisnikId",
                table: "Stomatolozi");

            migrationBuilder.DropIndex(
                name: "IX_Pacijenti_KorisnikId",
                table: "Pacijenti");

            migrationBuilder.DropIndex(
                name: "IX_MedicinskoOsoblje_KorisnikId",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Stomatolozi");

            migrationBuilder.DropColumn(
                name: "Titula",
                table: "Stomatolozi");

            migrationBuilder.DropColumn(
                name: "DatumRodjenja",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropColumn(
                name: "Titula",
                table: "MedicinskoOsoblje");

            migrationBuilder.AddColumn<bool>(
                name: "Aktivan",
                table: "Stomatolozi",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BrojZiroRacuna",
                table: "Stomatolozi",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogId",
                table: "Stomatolozi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TitulaID",
                table: "Stomatolozi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AlergijaNaLijek",
                table: "Pacijenti",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Aparatic",
                table: "Pacijenti",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogId",
                table: "Pacijenti",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Napomena",
                table: "Pacijenti",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Navlake",
                table: "Pacijenti",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Proteza",
                table: "Pacijenti",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Terapija",
                table: "Pacijenti",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Aktivan",
                table: "MedicinskoOsoblje",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BrojZiroRacuna",
                table: "MedicinskoOsoblje",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogId",
                table: "MedicinskoOsoblje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OpisPosla",
                table: "MedicinskoOsoblje",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TitulaID",
                table: "MedicinskoOsoblje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    DrzavaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.DrzavaId);
                });

            migrationBuilder.CreateTable(
                name: "Lijek",
                columns: table => new
                {
                    LijekId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lijek", x => x.LijekId);
                });

            migrationBuilder.CreateTable(
                name: "Materijal",
                columns: table => new
                {
                    MaterijalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    Opis = table.Column<string>(maxLength: 300, nullable: false),
                    Vrsta = table.Column<string>(maxLength: 100, nullable: false),
                    Proizvodjac = table.Column<string>(maxLength: 100, nullable: false),
                    Kolicina = table.Column<decimal>(type: "DECIMAL(18,1)", nullable: false),
                    MjernaJedinica = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materijal", x => x.MaterijalId);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    TerminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacijentId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Vrijeme = table.Column<DateTime>(nullable: false),
                    Razlog = table.Column<string>(maxLength: 200, nullable: false),
                    Hitan = table.Column<bool>(type: "BIT", nullable: false),
                    Odobren = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.TerminId);
                    table.ForeignKey(
                        name: "FK_Termini_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "PacijentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Titule",
                columns: table => new
                {
                    TitulaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titule", x => x.TitulaId);
                });

            migrationBuilder.CreateTable(
                name: "Usluga",
                columns: table => new
                {
                    UslugaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluga", x => x.UslugaId);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    GradId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrzavaId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    PostanskiBroj = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.GradId);
                    table.ForeignKey(
                        name: "FK_Grad_Drzava_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzava",
                        principalColumn: "DrzavaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terapija",
                columns: table => new
                {
                    TerapijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LijekId = table.Column<int>(nullable: false),
                    VrstaTerapije = table.Column<string>(maxLength: 100, nullable: false),
                    Kolicina = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terapija", x => x.TerapijaId);
                    table.ForeignKey(
                        name: "FK_Terapija_Lijek_LijekId",
                        column: x => x.LijekId,
                        principalTable: "Lijek",
                        principalColumn: "LijekId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RacunStavke",
                columns: table => new
                {
                    RacunStavkeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterijalId = table.Column<int>(nullable: false),
                    Kolicina = table.Column<decimal>(type: "DECIMAL(18,1)", nullable: false),
                    Cijena = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacunStavke", x => x.RacunStavkeId);
                    table.ForeignKey(
                        name: "FK_RacunStavke_Materijal_MaterijalId",
                        column: x => x.MaterijalId,
                        principalTable: "Materijal",
                        principalColumn: "MaterijalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UlazStavke",
                columns: table => new
                {
                    UlazStavkeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterijalId = table.Column<int>(nullable: false),
                    Kolicina = table.Column<decimal>(type: "DECIMAL(18,1)", nullable: false),
                    Cijena = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlazStavke", x => x.UlazStavkeId);
                    table.ForeignKey(
                        name: "FK_UlazStavke_Materijal_MaterijalId",
                        column: x => x.MaterijalId,
                        principalTable: "Materijal",
                        principalColumn: "MaterijalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalozi",
                columns: table => new
                {
                    KorisnickiNalogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(maxLength: 100, nullable: false),
                    Prezime = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 320, nullable: false),
                    LozinkaHash = table.Column<string>(maxLength: 100, nullable: false),
                    LozinkaSalt = table.Column<string>(maxLength: 50, nullable: false),
                    Permisije = table.Column<byte>(type: "TINYINT", nullable: false),
                    Kreirano = table.Column<DateTime>(nullable: false),
                    JMBG = table.Column<string>(maxLength: 13, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    Mobitel = table.Column<string>(maxLength: 30, nullable: false),
                    Adresa = table.Column<string>(maxLength: 200, nullable: false),
                    GradId = table.Column<int>(nullable: false),
                    Spol = table.Column<string>(maxLength: 10, nullable: false),
                    Slika = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalozi", x => x.KorisnickiNalogId);
                    table.ForeignKey(
                        name: "FK_KorisnickiNalozi_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pregledi",
                columns: table => new
                {
                    PregledId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerminId = table.Column<int>(nullable: false),
                    PacijentId = table.Column<int>(nullable: false),
                    TerapijaId = table.Column<int>(nullable: false),
                    TrajanjePregleda = table.Column<short>(type: "SMALLINT", nullable: false),
                    Napomena = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregledi", x => x.PregledId);
                    table.ForeignKey(
                        name: "FK_Pregledi_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "PacijentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pregledi_Terapija_TerapijaId",
                        column: x => x.TerapijaId,
                        principalTable: "Terapija",
                        principalColumn: "TerapijaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pregledi_Termini_TerminId",
                        column: x => x.TerminId,
                        principalTable: "Termini",
                        principalColumn: "TerminId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UlazUSkladiste",
                columns: table => new
                {
                    UlazUSkladisteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicinskoOsobljeId = table.Column<int>(nullable: false),
                    UlazStavkeId = table.Column<int>(nullable: false),
                    BrojFakture = table.Column<string>(maxLength: 50, nullable: false),
                    Datum = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    IznosRacuna = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PDV = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlazUSkladiste", x => x.UlazUSkladisteID);
                    table.ForeignKey(
                        name: "FK_UlazUSkladiste_MedicinskoOsoblje_MedicinskoOsobljeId",
                        column: x => x.MedicinskoOsobljeId,
                        principalTable: "MedicinskoOsoblje",
                        principalColumn: "MedicinskoOsoboljeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UlazUSkladiste_UlazStavke_UlazStavkeId",
                        column: x => x.UlazStavkeId,
                        principalTable: "UlazStavke",
                        principalColumn: "UlazStavkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AdministratorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickiNalogId = table.Column<int>(nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(nullable: false),
                    OpisPosla = table.Column<string>(maxLength: 200, nullable: false),
                    BrojZiroRacuna = table.Column<string>(maxLength: 20, nullable: false),
                    Aktivan = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdministratorId);
                    table.ForeignKey(
                        name: "FK_Administrator_KorisnickiNalozi_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromjenaLozinkes",
                columns: table => new
                {
                    PromjenaLozinkeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrijednost = table.Column<string>(maxLength: 30, nullable: false),
                    KorisnickiNalogID = table.Column<int>(nullable: false),
                    DatumPromjene = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromjenaLozinkes", x => x.PromjenaLozinkeID);
                    table.ForeignKey(
                        name: "FK_PromjenaLozinkes_KorisnickiNalozi_KorisnickiNalogID",
                        column: x => x.KorisnickiNalogID,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokeni",
                columns: table => new
                {
                    TokenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrijednost = table.Column<string>(maxLength: 50, nullable: false),
                    KorisnickiNalogId = table.Column<int>(nullable: false),
                    Kreirano = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokeni", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_Tokeni_KorisnickiNalozi_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzvrsenaUsluga",
                columns: table => new
                {
                    IzvrsenaUslugaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PregledId = table.Column<int>(nullable: false),
                    UslugaId = table.Column<int>(nullable: false),
                    Cijena = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzvrsenaUsluga", x => x.IzvrsenaUslugaId);
                    table.ForeignKey(
                        name: "FK_IzvrsenaUsluga_Pregledi_PregledId",
                        column: x => x.PregledId,
                        principalTable: "Pregledi",
                        principalColumn: "PregledId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzvrsenaUsluga_Usluga_UslugaId",
                        column: x => x.UslugaId,
                        principalTable: "Usluga",
                        principalColumn: "UslugaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicinskiKarton",
                columns: table => new
                {
                    MedicinskiKartonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzvrsenaUslugaId = table.Column<int>(nullable: false),
                    TerapijaId = table.Column<int>(nullable: false),
                    PacijentId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinskiKarton", x => x.MedicinskiKartonId);
                    table.ForeignKey(
                        name: "FK_MedicinskiKarton_IzvrsenaUsluga_IzvrsenaUslugaId",
                        column: x => x.IzvrsenaUslugaId,
                        principalTable: "IzvrsenaUsluga",
                        principalColumn: "IzvrsenaUslugaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinskiKarton_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "PacijentId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MedicinskiKarton_Terapija_TerapijaId",
                        column: x => x.TerapijaId,
                        principalTable: "Terapija",
                        principalColumn: "TerapijaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    RacunId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzvrsenaUslugaId = table.Column<int>(nullable: false),
                    MedicinskoOsobljeId = table.Column<int>(nullable: false),
                    PacijentId = table.Column<int>(nullable: false),
                    RacunStavkeId = table.Column<int>(nullable: false),
                    DatumIzdavanjaRacuna = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    Uplaceno = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racun", x => x.RacunId);
                    table.ForeignKey(
                        name: "FK_Racun_IzvrsenaUsluga_IzvrsenaUslugaId",
                        column: x => x.IzvrsenaUslugaId,
                        principalTable: "IzvrsenaUsluga",
                        principalColumn: "IzvrsenaUslugaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racun_MedicinskoOsoblje_MedicinskoOsobljeId",
                        column: x => x.MedicinskoOsobljeId,
                        principalTable: "MedicinskoOsoblje",
                        principalColumn: "MedicinskoOsoboljeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racun_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "PacijentId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Racun_RacunStavke_RacunStavkeId",
                        column: x => x.RacunStavkeId,
                        principalTable: "RacunStavke",
                        principalColumn: "RacunStavkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stomatolozi_KorisnickiNalogId",
                table: "Stomatolozi",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Stomatolozi_TitulaID",
                table: "Stomatolozi",
                column: "TitulaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pacijenti_KorisnickiNalogId",
                table: "Pacijenti",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskoOsoblje_KorisnickiNalogId",
                table: "MedicinskoOsoblje",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskoOsoblje_TitulaID",
                table: "MedicinskoOsoblje",
                column: "TitulaID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_KorisnickiNalogId",
                table: "Administrator",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Grad_DrzavaId",
                table: "Grad",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_IzvrsenaUsluga_PregledId",
                table: "IzvrsenaUsluga",
                column: "PregledId");

            migrationBuilder.CreateIndex(
                name: "IX_IzvrsenaUsluga_UslugaId",
                table: "IzvrsenaUsluga",
                column: "UslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalozi_GradId",
                table: "KorisnickiNalozi",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskiKarton_IzvrsenaUslugaId",
                table: "MedicinskiKarton",
                column: "IzvrsenaUslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskiKarton_PacijentId",
                table: "MedicinskiKarton",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskiKarton_TerapijaId",
                table: "MedicinskiKarton",
                column: "TerapijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregledi_PacijentId",
                table: "Pregledi",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregledi_TerapijaId",
                table: "Pregledi",
                column: "TerapijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregledi_TerminId",
                table: "Pregledi",
                column: "TerminId");

            migrationBuilder.CreateIndex(
                name: "IX_PromjenaLozinkes_KorisnickiNalogID",
                table: "PromjenaLozinkes",
                column: "KorisnickiNalogID");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_IzvrsenaUslugaId",
                table: "Racun",
                column: "IzvrsenaUslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_MedicinskoOsobljeId",
                table: "Racun",
                column: "MedicinskoOsobljeId");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_PacijentId",
                table: "Racun",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_RacunStavkeId",
                table: "Racun",
                column: "RacunStavkeId");

            migrationBuilder.CreateIndex(
                name: "IX_RacunStavke_MaterijalId",
                table: "RacunStavke",
                column: "MaterijalId");

            migrationBuilder.CreateIndex(
                name: "IX_Terapija_LijekId",
                table: "Terapija",
                column: "LijekId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_PacijentId",
                table: "Termini",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokeni_KorisnickiNalogId",
                table: "Tokeni",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazStavke_MaterijalId",
                table: "UlazStavke",
                column: "MaterijalId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazUSkladiste_MedicinskoOsobljeId",
                table: "UlazUSkladiste",
                column: "MedicinskoOsobljeId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazUSkladiste_UlazStavkeId",
                table: "UlazUSkladiste",
                column: "UlazStavkeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinskoOsoblje_KorisnickiNalozi_KorisnickiNalogId",
                table: "MedicinskoOsoblje",
                column: "KorisnickiNalogId",
                principalTable: "KorisnickiNalozi",
                principalColumn: "KorisnickiNalogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinskoOsoblje_Titule_TitulaID",
                table: "MedicinskoOsoblje",
                column: "TitulaID",
                principalTable: "Titule",
                principalColumn: "TitulaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacijenti_KorisnickiNalozi_KorisnickiNalogId",
                table: "Pacijenti",
                column: "KorisnickiNalogId",
                principalTable: "KorisnickiNalozi",
                principalColumn: "KorisnickiNalogId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Stomatolozi_KorisnickiNalozi_KorisnickiNalogId",
                table: "Stomatolozi",
                column: "KorisnickiNalogId",
                principalTable: "KorisnickiNalozi",
                principalColumn: "KorisnickiNalogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stomatolozi_Titule_TitulaID",
                table: "Stomatolozi",
                column: "TitulaID",
                principalTable: "Titule",
                principalColumn: "TitulaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinskoOsoblje_KorisnickiNalozi_KorisnickiNalogId",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinskoOsoblje_Titule_TitulaID",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacijenti_KorisnickiNalozi_KorisnickiNalogId",
                table: "Pacijenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Stomatolozi_KorisnickiNalozi_KorisnickiNalogId",
                table: "Stomatolozi");

            migrationBuilder.DropForeignKey(
                name: "FK_Stomatolozi_Titule_TitulaID",
                table: "Stomatolozi");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "MedicinskiKarton");

            migrationBuilder.DropTable(
                name: "PromjenaLozinkes");

            migrationBuilder.DropTable(
                name: "Racun");

            migrationBuilder.DropTable(
                name: "Titule");

            migrationBuilder.DropTable(
                name: "Tokeni");

            migrationBuilder.DropTable(
                name: "UlazUSkladiste");

            migrationBuilder.DropTable(
                name: "IzvrsenaUsluga");

            migrationBuilder.DropTable(
                name: "RacunStavke");

            migrationBuilder.DropTable(
                name: "KorisnickiNalozi");

            migrationBuilder.DropTable(
                name: "UlazStavke");

            migrationBuilder.DropTable(
                name: "Pregledi");

            migrationBuilder.DropTable(
                name: "Usluga");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "Materijal");

            migrationBuilder.DropTable(
                name: "Terapija");

            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "Drzava");

            migrationBuilder.DropTable(
                name: "Lijek");

            migrationBuilder.DropIndex(
                name: "IX_Stomatolozi_KorisnickiNalogId",
                table: "Stomatolozi");

            migrationBuilder.DropIndex(
                name: "IX_Stomatolozi_TitulaID",
                table: "Stomatolozi");

            migrationBuilder.DropIndex(
                name: "IX_Pacijenti_KorisnickiNalogId",
                table: "Pacijenti");

            migrationBuilder.DropIndex(
                name: "IX_MedicinskoOsoblje_KorisnickiNalogId",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropIndex(
                name: "IX_MedicinskoOsoblje_TitulaID",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropColumn(
                name: "Aktivan",
                table: "Stomatolozi");

            migrationBuilder.DropColumn(
                name: "BrojZiroRacuna",
                table: "Stomatolozi");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogId",
                table: "Stomatolozi");

            migrationBuilder.DropColumn(
                name: "TitulaID",
                table: "Stomatolozi");

            migrationBuilder.DropColumn(
                name: "AlergijaNaLijek",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "Aparatic",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogId",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "Napomena",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "Navlake",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "Proteza",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "Terapija",
                table: "Pacijenti");

            migrationBuilder.DropColumn(
                name: "Aktivan",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropColumn(
                name: "BrojZiroRacuna",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogId",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropColumn(
                name: "OpisPosla",
                table: "MedicinskoOsoblje");

            migrationBuilder.DropColumn(
                name: "TitulaID",
                table: "MedicinskoOsoblje");

            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Stomatolozi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titula",
                table: "Stomatolozi",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumRodjenja",
                table: "Pacijenti",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Pacijenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "MedicinskoOsoblje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titula",
                table: "MedicinskoOsoblje",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobitel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stomatolozi_KorisnikId",
                table: "Stomatolozi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacijenti_KorisnikId",
                table: "Pacijenti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskoOsoblje_KorisnikId",
                table: "MedicinskoOsoblje",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinskoOsoblje_Korisnici_KorisnikId",
                table: "MedicinskoOsoblje",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacijenti_Korisnici_KorisnikId",
                table: "Pacijenti",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stomatolozi_Korisnici_KorisnikId",
                table: "Stomatolozi",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
