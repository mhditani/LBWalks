using LBWalksAPI.Models.Domain;

namespace LBWalksAPI.Repository.IRepository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetALlAsync();
        Task<Region?> GetByIdAsync(Guid id);
        
        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id,Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
}
