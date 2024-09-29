﻿using AutoMapper;
using LBWalksAPI.CustomActionFilter;
using LBWalksAPI.Data;
using LBWalksAPI.Models.Domain;
using LBWalksAPI.Models.DTO;
using LBWalksAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly LBWalksDbContext db;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(LBWalksDbContext db, IRegionRepository regionRepository, IMapper mapper)
        {
            this.db = db;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetALlAsync();

            //var regionsDto = new List<RegionDTO>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDTO()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    });
            //}

            //Map Domain Model to DTO
             var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);
            return Ok(regionsDto); 
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //var regionDto = new RegionDTO
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};
            return Ok(mapper.Map<RegionDTO>(regionDomain));
        }




        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
           
                var regionDomain = mapper.Map<Region>(createRegionDto);
                regionDomain = await regionRepository.CreateAsync(regionDomain);
                var regionDto = mapper.Map<RegionDTO>(regionDomain);
                return CreatedAtAction(nameof(GetById), new { id = regionDomain.Id }, regionDto);
            
        


            //var regionDomain = new Region
            //{
            //    Code = createRegionDto.Code,
            //    Name = createRegionDto.Name,
            //    RegionImageUrl = createRegionDto.RegionImageUrl,
            //};


            //var regionDto = new RegionDTO
            //{
            //    Id=regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};

        }





        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            
                var regionDomain = mapper.Map<Region>(updateRegionDto);
                regionDomain = await regionRepository.UpdateAsync(id, regionDomain);
                if (regionDomain == null)
                {
                    return NotFound();
                }
                var regionDto = mapper.Map<UpdateRegionDto>(regionDomain);
                return Ok(regionDto);

       
            //var regionDomain = new Region
            //{
            //    Code= updateRegionDto.Code,
            //    Name = updateRegionDto.Name,
            //    RegionImageUrl = updateRegionDto.RegionImageUrl,
            //};





            //var regionDto = new RegionDTO
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

        }






        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var regionDomain =await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDTO> (regionDomain);
            //var regionDto = new RegionDTO
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

            return Ok(regionDto);
        }
    }
}
