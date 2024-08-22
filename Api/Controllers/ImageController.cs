using Application.ProductServices;
using Domain.Dtos;
using Application.ImageService;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/image")]

    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageService _imageService;

        public ImageController(ILogger<ImageController> logger, IImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }
        [HttpPost("upload")]
        public async Task<ActionResult<EBuyResponse>> upload(IFormFile file)
        {
            var result = await _imageService.UploadImage(file);
            return Ok(result);
        }
        [HttpPost("uploads")]
        public async Task<ActionResult<EBuyResponse>> uploads(List<IFormFile> file)
        {
            var result = await _imageService.UploadManyImage(file);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<EBuyResponse>> Delete(Guid Id)
        {
            var result = await _imageService.Delete(Id);
            return Ok(result);
        }
        [HttpGet]
        [Route("/image={imgaeId}")]

        public async Task<ActionResult<EBuyResponse<ImageResponseDto>>> GetImage(Guid imgaeId)
        {
            {
                var results = await _imageService.GetImage(imgaeId);
                return Ok(results);
            }
        }
        [HttpGet]

        public async Task<ActionResult<EBuyResponse<List<ImageResponseDto>>>> GetAll()
        {
            {
                var results = await _imageService.GetAll();
                return Ok(results);
            }
        }
    }
}
