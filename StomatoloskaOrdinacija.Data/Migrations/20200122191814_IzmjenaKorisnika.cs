using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class IzmjenaKorisnika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "DatumZaposlenja",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "Titula",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "DatumRodjenja",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "Stomatolog_DatumZaposlenja",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "Stomatolog_Titula",
                table: "Korisnici");

            migrationBuilder.CreateTable(
                name: "MedicinskoOsoblje",
                columns: table => new
                {
                    MedicinskoOsoboljeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    Titula = table.Column<string>(maxLength: 100, nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinskoOsoblje", x => x.MedicinskoOsoboljeId);
                    table.ForeignKey(
                        name: "FK_MedicinskoOsoblje_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pacijenti",
                columns: table => new
                {
                    PacijentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    DatumRodjenja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacijenti", x => x.PacijentId);
                    table.ForeignKey(
                        name: "FK_Pacijenti_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stomatolozi",
                columns: table => new
                {
                    StomatologId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    Titula = table.Column<string>(maxLength: 100, nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stomatolozi", x => x.StomatologId);
                    table.ForeignKey(
                        name: "FK_Stomatolozi_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskoOsoblje_KorisnikId",
                table: "MedicinskoOsoblje",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacijenti_KorisnikId",
                table: "Pacijenti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Stomatolozi_KorisnikId",
                table: "Stomatolozi",
                column: "KorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicinskoOsoblje");

            migrationBuilder.DropTable(
                name: "Pacijenti");

            migrationBuilder.DropTable(
                name: "Stomatolozi");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Korisnici",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumZaposlenja",
                table: "Korisnici",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titula",
                table: "Korisnici",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumRodjenja",
                table: "Korisnici",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Stomatolog_DatumZaposlenja",
                table: "Korisnici",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stomatolog_Titula",
                table: "Korisnici",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
