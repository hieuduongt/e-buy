using Domain.Dtos;
using Shared.Common;

namespace E_BUY.ImageService
{
    public interface IImageService
    {
        public Task<EBuyResponse<List<ImageResponseDto>>> GetAll();

        public Task<EBuyResponse<List<ImageResponseDto>>> Get(Guid productId);

        public Task<EBuyResponse<ImageResponseDto>> GetImage(Guid imageId);

        public Task<EBuyResponse> Create(ImageRequestDto imageRequestDto);

        public Task<EBuyResponse> Delete(Guid imgId);


    }
}
