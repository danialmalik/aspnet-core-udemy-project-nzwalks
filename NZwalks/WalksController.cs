using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks
{
    // /api/walks/
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;

        }

        // CREATE Walk
        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        // GET Walks
        // GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDto>>(walks));
        }

        // GET Walk by ID
        // GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }

        // UPDATE Walk by ID
        // PUT: /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var walk = mapper.Map<Walk>(updateWalkRequestDto);

            walk = await walkRepository.UpdateAsync(id, walk);

            if (walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walk));
        }

        // DELETE walk by ID
        // DELETE: /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }


    }
}
