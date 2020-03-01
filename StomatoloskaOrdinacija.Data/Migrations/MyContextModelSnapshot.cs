﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StomatoloskaOrdinacija.Data;

namespace StomatoloskaOrdinacija.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Administrator", b =>
                {
                    b.Property<int>("AdministratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aktivan")
                        .HasColumnType("BIT");

                    b.Property<string>("BrojZiroRacuna")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("KorisnickiNalogId")
                        .HasColumnType("int");

                    b.Property<string>("OpisPosla")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("AdministratorId");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Dijagnoza", b =>
                {
                    b.Property<int>("DijagnozaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("DijagnozaId");

                    b.ToTable("Dijagnoza");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Drzava", b =>
                {
                    b.Property<int>("DrzavaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("DrzavaId");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Grad", b =>
                {
                    b.Property<int>("GradId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrzavaId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("PostanskiBroj")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("GradId");

                    b.HasIndex("DrzavaId");

                    b.ToTable("Grad");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.IzvrsenaUsluga", b =>
                {
                    b.Property<int>("IzvrsenaUslugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cijena")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("PregledId")
                        .HasColumnType("int");

                    b.Property<int>("UslugaId")
                        .HasColumnType("int");

                    b.HasKey("IzvrsenaUslugaId");

                    b.HasIndex("PregledId");

                    b.HasIndex("UslugaId");

                    b.ToTable("IzvrsenaUsluga");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", b =>
                {
                    b.Property<int>("KorisnickiNalogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("SMALLDATETIME");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(320)")
                        .HasMaxLength(320);

                    b.Property<int>("GradId")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<DateTime>("Kreirano")
                        .HasColumnType("datetime2");

                    b.Property<string>("LozinkaHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LozinkaSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Mobitel")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<byte>("Permisije")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Slika")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Spol")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("KorisnickiNalogId");

                    b.HasIndex("GradId");

                    b.ToTable("KorisnickiNalozi");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Lijek", b =>
                {
                    b.Property<int>("LijekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("LijekId");

                    b.ToTable("Lijek");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Materijal", b =>
                {
                    b.Property<int>("MaterijalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Kolicina")
                        .HasColumnType("DECIMAL(18,1)");

                    b.Property<string>("MjernaJedinica")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Proizvodjac")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Vrsta")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MaterijalId");

                    b.ToTable("Materijal");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.MedicinskiKarton", b =>
                {
                    b.Property<int>("MedicinskiKartonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("IzvrsenaUslugaId")
                        .HasColumnType("int");

                    b.Property<string>("Napomena")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("PacijentId")
                        .HasColumnType("int");

                    b.HasKey("MedicinskiKartonId");

                    b.HasIndex("IzvrsenaUslugaId");

                    b.HasIndex("PacijentId");

                    b.ToTable("MedicinskiKarton");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.MedicinskoOsoblje", b =>
                {
                    b.Property<int>("MedicinskoOsoboljeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aktivan")
                        .HasColumnType("BIT");

                    b.Property<string>("BrojZiroRacuna")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("KorisnickiNalogId")
                        .HasColumnType("int");

                    b.Property<string>("OpisPosla")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("TitulaID")
                        .HasColumnType("int");

                    b.HasKey("MedicinskoOsoboljeId");

                    b.HasIndex("KorisnickiNalogId");

                    b.HasIndex("TitulaID");

                    b.ToTable("MedicinskoOsoblje");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Pacijent", b =>
                {
                    b.Property<int>("PacijentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AlergijaNaLijek")
                        .HasColumnType("BIT");

                    b.Property<bool>("Aparatic")
                        .HasColumnType("BIT");

                    b.Property<int>("KorisnickiNalogId")
                        .HasColumnType("int");

                    b.Property<bool>("Navlake")
                        .HasColumnType("BIT");

                    b.Property<bool>("Proteza")
                        .HasColumnType("BIT");

                    b.Property<bool>("Terapija")
                        .HasColumnType("BIT");

                    b.HasKey("PacijentId");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("Pacijenti");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Pregled", b =>
                {
                    b.Property<int>("PregledId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Napomena")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("PacijentId")
                        .HasColumnType("int");

                    b.Property<int>("StomatologId")
                        .HasColumnType("int");

                    b.Property<int>("TerapijaId")
                        .HasColumnType("int");

                    b.Property<int>("TerminId")
                        .HasColumnType("int");

                    b.Property<short>("TrajanjePregleda")
                        .HasColumnType("SMALLINT");

                    b.Property<int>("UspostavljenaDijagnozaId")
                        .HasColumnType("int");

                    b.HasKey("PregledId");

                    b.HasIndex("PacijentId");

                    b.HasIndex("StomatologId");

                    b.HasIndex("TerapijaId");

                    b.HasIndex("TerminId");

                    b.HasIndex("UspostavljenaDijagnozaId");

                    b.ToTable("Pregledi");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.PromjenaLozinke", b =>
                {
                    b.Property<int>("PromjenaLozinkeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumPromjene")
                        .HasColumnType("SMALLDATETIME");

                    b.Property<int>("KorisnickiNalogID")
                        .HasColumnType("int");

                    b.Property<string>("Vrijednost")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("PromjenaLozinkeID");

                    b.HasIndex("KorisnickiNalogID");

                    b.ToTable("PromjenaLozinkes");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Racun", b =>
                {
                    b.Property<int>("RacunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIzdavanjaRacuna")
                        .HasColumnType("SMALLDATETIME");

                    b.Property<int>("IzvrsenaUslugaId")
                        .HasColumnType("int");

                    b.Property<int>("MedicinskoOsobljeId")
                        .HasColumnType("int");

                    b.Property<int>("PacijentId")
                        .HasColumnType("int");

                    b.Property<bool>("Uplaceno")
                        .HasColumnType("BIT");

                    b.HasKey("RacunId");

                    b.HasIndex("IzvrsenaUslugaId");

                    b.HasIndex("MedicinskoOsobljeId");

                    b.HasIndex("PacijentId");

                    b.ToTable("Racun");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.RacunStavke", b =>
                {
                    b.Property<int>("RacunStavkeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cijena")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Kolicina")
                        .HasColumnType("DECIMAL(18,1)");

                    b.Property<int>("MaterijalId")
                        .HasColumnType("int");

                    b.Property<int>("RacunId")
                        .HasColumnType("int");

                    b.HasKey("RacunStavkeId");

                    b.HasIndex("MaterijalId");

                    b.HasIndex("RacunId");

                    b.ToTable("RacunStavke");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Stomatolog", b =>
                {
                    b.Property<int>("StomatologId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aktivan")
                        .HasColumnType("BIT");

                    b.Property<string>("BrojZiroRacuna")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("KorisnickiNalogId")
                        .HasColumnType("int");

                    b.Property<int>("TitulaID")
                        .HasColumnType("int");

                    b.HasKey("StomatologId");

                    b.HasIndex("KorisnickiNalogId");

                    b.HasIndex("TitulaID");

                    b.ToTable("Stomatolozi");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Terapija", b =>
                {
                    b.Property<int>("TerapijaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Kolicina")
                        .HasColumnType("TINYINT");

                    b.Property<int>("LijekId")
                        .HasColumnType("int");

                    b.Property<string>("VrstaTerapije")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("TerapijaId");

                    b.HasIndex("LijekId");

                    b.ToTable("Terapija");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Termin", b =>
                {
                    b.Property<int>("TerminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Hitan")
                        .HasColumnType("BIT");

                    b.Property<bool>("Odobren")
                        .HasColumnType("BIT");

                    b.Property<int>("PacijentId")
                        .HasColumnType("int");

                    b.Property<string>("Razlog")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("Vrijeme")
                        .HasColumnType("datetime2");

                    b.HasKey("TerminId");

                    b.HasIndex("PacijentId");

                    b.ToTable("Termini");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Titula", b =>
                {
                    b.Property<int>("TitulaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("TitulaId");

                    b.ToTable("Titule");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KorisnickiNalogId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Kreirano")
                        .HasColumnType("SMALLDATETIME");

                    b.Property<string>("Vrijednost")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("TokenId");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("Tokeni");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.UlazStavke", b =>
                {
                    b.Property<int>("UlazStavkeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cijena")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Kolicina")
                        .HasColumnType("DECIMAL(18,1)");

                    b.Property<int>("MaterijalId")
                        .HasColumnType("int");

                    b.Property<int>("UlazUSkladisteId")
                        .HasColumnType("int");

                    b.HasKey("UlazStavkeId");

                    b.HasIndex("MaterijalId");

                    b.HasIndex("UlazUSkladisteId");

                    b.ToTable("UlazStavke");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.UlazUSkladiste", b =>
                {
                    b.Property<int>("UlazUSkladisteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrojFakture")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("SMALLDATETIME");

                    b.Property<decimal>("IznosRacuna")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("MedicinskoOsobljeId")
                        .HasColumnType("int");

                    b.Property<decimal>("PDV")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("UlazUSkladisteID");

                    b.HasIndex("MedicinskoOsobljeId");

                    b.ToTable("UlazUSkladiste");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Usluga", b =>
                {
                    b.Property<int>("UslugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("UslugaId");

                    b.ToTable("Usluga");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.UspostavljenaDijagnoza", b =>
                {
                    b.Property<int>("UspostavljenaDijagnozaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DijagnozaId")
                        .HasColumnType("int");

                    b.Property<byte>("JacinaDijagnoze")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Napomena")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.HasKey("UspostavljenaDijagnozaId");

                    b.HasIndex("DijagnozaId");

                    b.ToTable("UspostavljenaDijagnoza");
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Administrator", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Grad", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("DrzavaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.IzvrsenaUsluga", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Pregled", "Pregled")
                        .WithMany()
                        .HasForeignKey("PregledId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Usluga", "Usluga")
                        .WithMany()
                        .HasForeignKey("UslugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.MedicinskiKarton", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.IzvrsenaUsluga", "IzvrsenaUsluga")
                        .WithMany()
                        .HasForeignKey("IzvrsenaUslugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Pacijent", "Pacijent")
                        .WithMany()
                        .HasForeignKey("PacijentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.MedicinskoOsoblje", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Titula", "Titula")
                        .WithMany()
                        .HasForeignKey("TitulaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Pacijent", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Pregled", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Pacijent", "Pacijent")
                        .WithMany()
                        .HasForeignKey("PacijentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Stomatolog", "Stomatolog")
                        .WithMany()
                        .HasForeignKey("StomatologId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Terapija", "Terapija")
                        .WithMany()
                        .HasForeignKey("TerapijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Termin", "Termin")
                        .WithMany()
                        .HasForeignKey("TerminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.UspostavljenaDijagnoza", "UspostavljenaDijagnoza")
                        .WithMany()
                        .HasForeignKey("UspostavljenaDijagnozaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.PromjenaLozinke", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Racun", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.IzvrsenaUsluga", "IzvrsenaUsluga")
                        .WithMany()
                        .HasForeignKey("IzvrsenaUslugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.MedicinskoOsoblje", "MedicinskoOsoblje")
                        .WithMany()
                        .HasForeignKey("MedicinskoOsobljeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Pacijent", "Pacijent")
                        .WithMany()
                        .HasForeignKey("PacijentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.RacunStavke", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Materijal", "Materijal")
                        .WithMany()
                        .HasForeignKey("MaterijalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Racun", "Racun")
                        .WithMany()
                        .HasForeignKey("RacunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Stomatolog", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Titula", "Titula")
                        .WithMany()
                        .HasForeignKey("TitulaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Terapija", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Lijek", "Lijek")
                        .WithMany()
                        .HasForeignKey("LijekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Termin", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Pacijent", "Pacijent")
                        .WithMany()
                        .HasForeignKey("PacijentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.Token", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.UlazStavke", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Materijal", "Materijal")
                        .WithMany()
                        .HasForeignKey("MaterijalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.UlazUSkladiste", "UlazUSkladiste")
                        .WithMany()
                        .HasForeignKey("UlazUSkladisteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.UlazUSkladiste", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.MedicinskoOsoblje", "MedicinskoOsoblje")
                        .WithMany()
                        .HasForeignKey("MedicinskoOsobljeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StomatoloskaOrdinacija.Data.EntityModels.UspostavljenaDijagnoza", b =>
                {
                    b.HasOne("StomatoloskaOrdinacija.Data.EntityModels.Dijagnoza", "Dijagnoza")
                        .WithMany()
                        .HasForeignKey("DijagnozaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
