using BackendTest.Command.EFInfraStructure.MapConfig;
using BackendTest.Domain;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Command.EFInfraStructure
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStatus> ItemStatus { get; set; }
        public DbSet<Status> Status { get; set; }

        internal AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemMap());
            modelBuilder.ApplyConfiguration(new ItemStatusMap());
            modelBuilder.ApplyConfiguration(new StatusMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();
        }
    }
}