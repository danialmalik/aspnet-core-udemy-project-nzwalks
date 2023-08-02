using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Data;


namespace NZWalks.API.Controllers
{
    // /api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        // GET ALL REGIONS
        // GET /api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

            // Map domain models to DTO
            var regionsDto = new List<RegionDto>();
            foreach(var regionDomain in regions)
            {
                regionsDto.Add(new RegionDto(){
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImgUrl = regionDomain.RegionImgUrl,
                });
            }
            // Return DTOs
            return Ok(regionsDto);
        }

        // GET REGION by ID
        // GET /api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Find using PK only.
            var region = dbContext.Regions.Find(id);

            // To search based on any field, use the FirstOrDefault method
            // var region = dbContext.Regions.FirstOrDefault(x=>x.id == id);

            if (region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImgUrl = region.RegionImgUrl,

            };

            return Ok(regionDto);
        }

        // POST Create new Region
        // POST /api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImgUrl = addRegionRequestDto.RegionImgUrl,
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImgUrl = regionDomainModel.RegionImgUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        // PUT Update Region
        // PUT /api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x=>x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImgUrl = updateRegionRequestDto.RegionImgUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImgUrl = regionDomainModel.RegionImgUrl,
            };

            return Ok(regionDto);
        }

        // DELETE Region
        // DELETE /api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x=>x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImgUrl = regionDomainModel.RegionImgUrl,
            };

            return Ok(regionDto);
        }
    }

}
