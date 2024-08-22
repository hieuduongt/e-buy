using Application.LikeServices;
using Application.ProductServices;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Shared.Common;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/like")]
    public class LikeController : ControllerBase
    {
        private readonly ILogger<LikeController> _logger;
        private readonly ILikeService _likeService;

        public LikeController(ILogger<LikeController> logger, ILikeService likeService)
        {
            _logger = logger;
            _likeService = likeService;
        }
        [HttpPost]
        public async Task<ActionResult<EBuyResponse>> Create([FromBody] RequesLikeDto request)
        {
            var result = await _likeService.CreateLike(request);

            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<EBuyResponse>> Delete(Guid productId)
        {
            var result = await _likeService.Delete(productId);
            return Ok(result);
        }
    }
}
