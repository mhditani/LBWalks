using AutoMapper;
using LBWalksAPI.Models.Domain;
using LBWalksAPI.Models.DTO;

namespace LBWalksAPI.Mapper
{
    public class AutoMapperProfiles : Profile   
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, CreateRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();


            CreateMap<Walk, CreateWalkDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkDto>().ReverseMap();


            CreateMap<Difficulty, DifficultyDto>().ReverseMap();


   

        }
    }
}
