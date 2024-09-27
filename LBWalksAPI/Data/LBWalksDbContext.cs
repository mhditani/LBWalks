using LBWalksAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LBWalksAPI.Data
{
    public class LBWalksDbContext : DbContext
    {
        public LBWalksDbContext(DbContextOptions options) : base(options)
        {
            
        }

         public DbSet<Walk> Walks { get; set; }

         public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }
    }
}
