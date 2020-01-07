using Microsoft.EntityFrameworkCore;

namespace StomatoloskaOrdinacija.Data
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> x) : base(x)
        {

        }
    }
}
