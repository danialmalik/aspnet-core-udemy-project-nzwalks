using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
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
            return Ok(regions);
        }

        // GET REGION by ID
        // GET /api/regions/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Find using PK only.
            var region = dbContext.Regions.Find(id);

            // To search based on any field, use the FindOrDefault method
            // var region = dbContext.Regions.FindOrDefault(x=>x.id == id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }

}
