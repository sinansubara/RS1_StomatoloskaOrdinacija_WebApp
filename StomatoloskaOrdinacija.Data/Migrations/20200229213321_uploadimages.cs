using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    public partial class uploadimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slika",
                table: "KorisnickiNalozi",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(MAX)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Slika",
                table: "KorisnickiNalozi",
                type: "VARBINARY(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");
        }
    }
}
