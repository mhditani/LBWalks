using LBWalksAPI.Data;
using LBWalksAPI.Models.Domain;
using LBWalksAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LBWalksAPI.Repository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly LBWalksDbContext db;

        public SQLWalkRepository(LBWalksDbContext db)
        {
            this.db = db;
        }






        public async Task<Walk> CreateAsync(Walk walk)
        {
            await db.Walks.AddAsync(walk);
            await db.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await db.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            db.Walks.Remove(existingWalk);
            await db.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int PageSize = 5)
        {
            var walks = db.Walks.Include("Difficulty").Include("Region").AsQueryable();     
            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * PageSize;

            return await walks.Skip(skipResults).Take(PageSize).ToListAsync();
            //return await db.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await db.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
         
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
           var existingWalk =  await db.Walks.FirstOrDefaultAsync(x =>x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            await db.SaveChangesAsync();
            return existingWalk;
        }
    }
}
