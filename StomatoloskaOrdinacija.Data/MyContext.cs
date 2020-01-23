using Microsoft.EntityFrameworkCore;
using StomatoloskaOrdinacija.Data.EntityModels;

namespace StomatoloskaOrdinacija.Data
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> x) : base(x)
        {

        }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Stomatolog> Stomatolozi { get; set; }
        public DbSet<MedicinskoOsoblje> MedicinskoOsoblje { get; set; }
        public DbSet<Pacijent> Pacijenti { get; set; }
        
    }
}
