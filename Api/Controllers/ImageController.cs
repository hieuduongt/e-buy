using Application.ProductServices;
using Domain.Dtos;
using E_BUY.ImageService;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace E_BUY.Controllers
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
        [HttpGet]
        [Route("/productId={productId}")]

        public async Task<ActionResult<EBuyResponse<List<ImageResponseDto>>>> Get(Guid productId)
        {
            {
                var results = await _imageService.Get(productId);
                return Ok(results);
            }
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
        [HttpPost]
        public async Task<ActionResult<EBuyResponse>>Create(ImageRequestDto reques)
        {
            var result = await _imageService.Create(reques);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<EBuyResponse>> Delete(Guid Id)
        {
            var result = await _imageService.Delete(Id);
            return Ok(result);
        }
    }
}
