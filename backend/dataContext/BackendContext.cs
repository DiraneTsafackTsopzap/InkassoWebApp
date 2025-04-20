using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.dataContext
{



    public class BackendContext : DbContext
    {
        public BackendContext(DbContextOptions<BackendContext> options) : base(options)
        {
        }
        public DbSet<Forderung> Forderung { get; set; }


        public DbSet<Kunde> Kunden { get; set; }

        public DbSet<Schuldner> Schuldner { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Optionel



        }


        // A delete en cas d'echec  : pr le unittest
        private readonly string connectionString;

        public BackendContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(connectionString);
            }
        }


    }
}
