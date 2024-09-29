using AutoMapper;
using LBWalksAPI.CustomActionFilter;
using LBWalksAPI.Models.Domain;
using LBWalksAPI.Models.DTO;
using LBWalksAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LBWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }







        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {
            

                var walkDomain = mapper.Map<Walk>(createWalkDto);
                await walkRepository.CreateAsync(walkDomain);

                return Ok(mapper.Map<WalkDto>(walkDomain));
     
        }







        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 5)
        {
           var walksDomain =  await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, PageSize);
            return Ok(mapper.Map<List<WalkDto>>(walksDomain)); 
        }






        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }











        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            
                var walkDomain = mapper.Map<Walk>(updateWalkDto);
                walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
                if (walkDomain == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDto>(walkDomain));
     
        }









        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var deletedDomain = await walkRepository.DeleteAsync(id);
            if (deletedDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedDomain));
        }
    }
}
