using LBWalksAPI.Data;
using LBWalksAPI.Models.Domain;
using LBWalksAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LBWalksAPI.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly LBWalksDbContext db;

        public SQLRegionRepository(LBWalksDbContext db)
        {
            this.db = db;
        }

        public async Task<Region> CreateAsync(Region region)
        {
             await db.Regions.AddAsync(region);
            await db.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var existingRegion = await db.Regions.FirstOrDefaultAsync( r => r.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            db.Regions.Remove(existingRegion);
            await db.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetALlAsync()
        {
           return await db.Regions.ToListAsync();
        }



        public async Task<Region?> GetByIdAsync(Guid id)
        {
          return await db.Regions.FirstOrDefaultAsync( r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await db.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await db.SaveChangesAsync();
            return existingRegion;
        }
    }
}
