using Microsoft.EntityFrameworkCore;

namespace HiLo.EfCore
{
    public class SampleDBContext : DbContext
    {
        public SampleDBContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            string connectionString = @"Server=tcp:127.0.0.1,5433;Initial Catalog=EFSampleDB;User Id=sa;Password=Pass@word";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.UseHiLo("DBSequenceHiLo");

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}