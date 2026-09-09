using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;

namespace AccesoDatos.Data
{
    public class AplicationDbContext : DbContext
    {
       
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Socio> Socio { get; set; }
        public DbSet<Alquiler> Alquiler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\databases\\exampleDB.db");
        }
    }
}
