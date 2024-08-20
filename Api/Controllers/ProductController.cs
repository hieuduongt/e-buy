using Api.Controllers;
using Application.CategoryServices;
using Application.ProductServices;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/product")]

    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpPost]
        public async Task<ActionResult<EBuyResponse>> Create([FromBody] ProductRequesDto request)
        {
            var result = await _productService.Create(request);

            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<EBuyResponse<Paginated<ProductResponseDto>>>> getAllProduct(string search = "", int page = 1, int pageSize = 20)
        {
            var result = await _productService.GetAll(search, page, pageSize);

            return Ok(result);
        }
        [HttpGet]
        [Route("{productId}")]
        public async Task<ActionResult<EBuyResponse<Paginated<ProductResponseDto>>>> getProduct(Guid productId)
        {
            var result = await _productService.Get(productId);

            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<EBuyResponse>> deleteProduct(Guid productId)
        {
            var result = await _productService.Delete(productId);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<EBuyResponse>> updateProduct(ProductRequesDto reques)
        {
            var result = await _productService.Update(reques);
            return Ok(result);
        }
        [HttpPatch]
        [Route("{productId}")]

        public async Task<ActionResult<EBuyResponse>> AtArchiveProduct(Guid productId)
        {
            var result = await _productService.Archive(productId);
            return Ok(result);
        }
    }
}
