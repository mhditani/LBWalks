using LBWalksAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LBWalksAPI.Data
{
    public class LBWalksDbContext : DbContext
    {
        public LBWalksDbContext(DbContextOptions<LBWalksDbContext> options) : base(options)
        {
            
        }

         public DbSet<Walk> Walks { get; set; }

         public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("8fa19201-af46-4777-831e-ed04970f0b6a"),
                    Name = "Easy"
                },

                  new Difficulty
                {
                    Id = Guid.Parse("67267179-0dae-41cf-9a6b-9a5d3355bcdc"),
                    Name = "Medium"
                },

                    new Difficulty
                {
                    Id = Guid.Parse("7b021da9-5874-4c98-ab23-8345a1d112a1"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}
