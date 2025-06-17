using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
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

        // POST: api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map the DTO to the Domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            var walk = await walkRepository.CreateAsync(walkDomainModel);

            // Map the Domain model back to the DTO
            var walkDto = mapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }

        // GET: api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            // Map the Domain models to DTOs
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);
            
            return Ok(walksDto);
        }

        // GET: api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel is null)
            {
                return NotFound();
            }

            // Map the Domain model to the DTO
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        // PUT: api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map the DTO to the Domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            var updatedWalk = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (updatedWalk is null)
            {
                return NotFound();
            }

            // Map the updated Domain model back to the DTO
            var updatedWalkDto = mapper.Map<WalkDto>(updatedWalk);

            return Ok(updatedWalkDto);

        }

        // DELETE: api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);

            if(deletedWalk is null)
            {
                return NotFound();
            }

            // Map the deleted Domain model to the DTO
            var deletedWalkDto = mapper.Map<WalkDto>(deletedWalk);
            return Ok(deletedWalkDto);

        }
    }
}
