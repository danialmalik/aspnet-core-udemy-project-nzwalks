using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Data;
using NZWalks.API.Repositories;
using AutoMapper;
using NZWalks.API.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    // /api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(
            NZWalksDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper autoMapperProfiles,
            ILogger<RegionsController> logger
        )
        {
            this.regionRepository = regionRepository;
            this.mapper = autoMapperProfiles;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        // GET /api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogDebug("GetAllRegions method was invoked.");
            var regions = await regionRepository.GetAllAsync();

            var regionsDto = mapper.Map<List<RegionDto>>(regions);
            // // Map domain models to DTO
            // var regionsDto = new List<RegionDto>();
            // foreach(var regionDomain in regions)
            // {
            //     regionsDto.Add(new RegionDto(){
            //         Id = regionDomain.Id,
            //         Code = regionDomain.Code,
            //         Name = regionDomain.Name,
            //         RegionImageUrl = regionDomain.RegionImageUrl,
            //     });
            // }

            string message = $"Finished GetAllRegions with data: {JsonSerializer.Serialize(regionsDto)}";
            logger.LogInformation(message: message);
            // Return DTOs
            return Ok(regionsDto);
        }

        // GET REGION by ID
        // GET /api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,

            };

            return Ok(regionDto);
        }

        // POST Create new Region
        // POST /api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        // PUT Update Region
        // PUT /api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        // DELETE Region
        // DELETE /api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }

}
