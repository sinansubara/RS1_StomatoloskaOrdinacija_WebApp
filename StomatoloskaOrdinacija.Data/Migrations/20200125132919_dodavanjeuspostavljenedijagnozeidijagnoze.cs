using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class dodavanjeuspostavljenedijagnozeidijagnoze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StomatologId",
                table: "Pregledi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dijagnoza",
                columns: table => new
                {
                    DijagnozaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dijagnoza", x => x.DijagnozaId);
                });

            migrationBuilder.CreateTable(
                name: "UspostavljenaDijagnoza",
                columns: table => new
                {
                    UspostavljenaDijagnozaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DijagnozaId = table.Column<int>(nullable: false),
                    JacinaDijagnoze = table.Column<byte>(type: "TINYINT", nullable: false),
                    Napomena = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UspostavljenaDijagnoza", x => x.UspostavljenaDijagnozaId);
                    table.ForeignKey(
                        name: "FK_UspostavljenaDijagnoza_Dijagnoza_DijagnozaId",
                        column: x => x.DijagnozaId,
                        principalTable: "Dijagnoza",
                        principalColumn: "DijagnozaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pregledi_StomatologId",
                table: "Pregledi",
                column: "StomatologId");

            migrationBuilder.CreateIndex(
                name: "IX_UspostavljenaDijagnoza_DijagnozaId",
                table: "UspostavljenaDijagnoza",
                column: "DijagnozaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregledi_Stomatolozi_StomatologId",
                table: "Pregledi",
                column: "StomatologId",
                principalTable: "Stomatolozi",
                principalColumn: "StomatologId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregledi_Stomatolozi_StomatologId",
                table: "Pregledi");

            migrationBuilder.DropTable(
                name: "UspostavljenaDijagnoza");

            migrationBuilder.DropTable(
                name: "Dijagnoza");

            migrationBuilder.DropIndex(
                name: "IX_Pregledi_StomatologId",
                table: "Pregledi");

            migrationBuilder.DropColumn(
                name: "StomatologId",
                table: "Pregledi");
        }
    }
}
