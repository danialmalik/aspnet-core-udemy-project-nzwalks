using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] {".jpg", ".jpeg", ".png"};
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension.");
            }

            if (request.File.Length > 10485760) // 10 MB
            {
                ModelState.AddModelError("file", "File size is more than 10MB.");
            }
        }
    }
}
