using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository repository;

        public ImagesController(IImageRepository repository)
        {
            this.repository = repository;
        }

        // POST: api/images/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {
                // Convert the DTO to a domain model
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription
                };

                // Use repository to upload the image
                await repository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto) 
        {
            var allowedFileTypes = new string[] {".jpg", ".jpeg", ".png"};
            if (allowedFileTypes.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if(imageUploadRequestDto.File.Length > 10485760) // 10 MB
            {
                ModelState.AddModelError("file", "File size exceeds the limit of 10 MB");
            }
        }
    }
}
