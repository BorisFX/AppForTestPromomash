using AppForTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppForTest.Infrastructure.Data
{

    public class AppForTestDbContext : DbContext
    {
        public AppForTestDbContext() { }

        public AppForTestDbContext(DbContextOptions<AppForTestDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCountrySelection> UserCountrySelections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}