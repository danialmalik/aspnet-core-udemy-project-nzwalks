using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;

        }
        public async Task<IActionResult> Index()
        {
            List<RegionDto> regions = new List<RegionDto>();
            try
            {

                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("http://localhost:5292/api/Regions");
                httpResponseMessage.EnsureSuccessStatusCode();

                regions.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {
                //Log exception;
                throw ex;
            }

            return View(regions);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:5292/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var responseMessage = await client.SendAsync(httpRequestMessage);
            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();

        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>($"http://localhost:5292/api/Regions/{id}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://localhost:5292/api/Regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.DeleteAsync($"http://localhost:5292/api/Regions/{request.Id}");
            httpResponseMessage.EnsureSuccessStatusCode();

            return RedirectToAction("Index", "Regions");
        }
    }
}
