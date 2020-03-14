using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class terminDatumVrijeme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datum",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "Vrijeme",
                table: "Termini");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumVrijeme",
                table: "Termini",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumVrijeme",
                table: "Termini");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "Termini",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Vrijeme",
                table: "Termini",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
