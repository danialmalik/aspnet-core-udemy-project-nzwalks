using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;


namespace NZWalks
{
    // /api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // GET ALL REGIONS
        // GET /api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Region",
                    Code = "TR",
                    RegionImgUrl = "https://images.pexels.com/photos/12587261/pexels-photo-12587261.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Region 2",
                    Code = "TR2",
                    RegionImgUrl = "https://images.pexels.com/photos/12587261/pexels-photo-12587261.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
            };

            return Ok(regions);
        }
    }

}
