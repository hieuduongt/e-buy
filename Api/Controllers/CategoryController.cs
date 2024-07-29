using Application.CategoryServices;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {

        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ILogger<CategoryController> logger, ICategoryServices categoryServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
        }

        [HttpPost]
        public async Task<ActionResult<EBuyResponse>> Create([FromBody] CategoryRequestDto request)
        {
            var result = await _categoryServices.Create(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<EBuyResponse>> Update([FromBody] CategoryRequestDto request)
        {
            var result = await _categoryServices.Update(request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<EBuyResponse>> Delete(Guid id)
        {
            var result = await _categoryServices.Delete(id);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<EBuyResponse>> Archive(Guid id)
        {
            var result = await _categoryServices.Archive(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<EBuyResponse<Paginated<CategoryResponseDto>>>> GetAll(string search = "", int page = 1, int pageSize = 20)
        {
            var result = await _categoryServices.GetAll(search, page, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EBuyResponse<CategoryResponseDto>>> Get(Guid id)
        {
            var result = await _categoryServices.Get(id);
            return Ok(result);
        }
    }
}
