using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;

namespace AccesoDatos.Data
{
    public class AplicationDbContext : DbContext
    {
       
        public DbSet<Libro> Libro { get; set; }
        public DbSet<Autor> Autor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\databases\\exampleDB.db");
        }
    }
}
