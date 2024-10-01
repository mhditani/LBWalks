using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LBWalksAPI.Data
{
    public class LBWalksAuthDbContext : IdentityDbContext
    {
        public LBWalksAuthDbContext(DbContextOptions<LBWalksAuthDbContext> options) : base(options)
        {
        }   



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "c9586eb9-8943-427a-9c75-ac6eb262343d";
            var writerRoleId = "8ab52981-14cb-48a2-ad2d-0d84d13d6259";

        var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
