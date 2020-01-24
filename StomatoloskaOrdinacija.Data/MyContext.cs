using Microsoft.EntityFrameworkCore;
using StomatoloskaOrdinacija.Data.EntityModels;

namespace StomatoloskaOrdinacija.Data
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> x) : base(x)
        {

        }
        public DbSet<KorisnickiNalog> KorisnickiNalogs { get; set; }
        public DbSet<Stomatolog> Stomatologs { get; set; }
        public DbSet<MedicinskoOsoblje> MedicinskoOsobljes { get; set; }
        public DbSet<Pacijent> Pacijents { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Drzava> Drzavas { get; set; }
        public DbSet<Grad> Grads { get; set; }
        public DbSet<IzvrsenaUsluga> IzvrsenaUslugas { get; set; }
        public DbSet<Lijek> Lijeks { get; set; }
        public DbSet<Materijal> Materijals { get; set; }
        public DbSet<MedicinskiKarton> MedicinskiKartons { get; set; }
        public DbSet<Pregled> Pregleds { get; set; }
        public DbSet<PromjenaLozinke> PromjenaLozinkes { get; set; }
        public DbSet<Racun> Racuns { get; set; }
        public DbSet<RacunStavke> RacunStavkes { get; set; }
        public DbSet<Terapija> Terapijas { get; set; }
        public DbSet<Termin> Termins { get; set; }
        public DbSet<Titula> Titulas { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<UlazStavke> UlazStavkes { get; set; }
        public DbSet<UlazUSkladiste> UlazUSkladistes { get; set; }
        public DbSet<Usluga> Uslugas { get; set; }
    }
}
