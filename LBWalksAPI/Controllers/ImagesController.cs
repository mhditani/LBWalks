using LBWalksAPI.Models.Domain;
using LBWalksAPI.Models.DTO;
using LBWalksAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LBWalksAPI.Controllers
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





        private void ValidateFileUpload(ImageUploadDto imageUploadDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png"};

            if (!allowedExtension.Contains(Path.GetExtension(imageUploadDto.File.FileName))  )
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }

            if (imageUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10mb, please upload a smaller file.");
            }
        }



        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto imageUploadDto)
        {
            ValidateFileUpload(imageUploadDto);

            if (ModelState.IsValid)
            {
                // Convert DTO to Domain Model
                var imageDomain = new Image
                {
                    File = imageUploadDto.File,
                    FileExtension = Path.GetExtension(imageUploadDto.File.FileName),
                    FileSizeInBytes = imageUploadDto.File.Length,
                    FileName = imageUploadDto.FileName,
                    FileDescription = imageUploadDto.FileDescription,
                };

                // User Repository to upload image
                await imageRepository.Upload(imageDomain);
                return Ok(imageDomain);

            }

            return BadRequest(ModelState);
        }
    }
}
