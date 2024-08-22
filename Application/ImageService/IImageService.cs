using Domain.Dtos;
using Shared.Common;
using Microsoft.AspNetCore.Http;
namespace Application.ImageService
{
    public interface IImageService
    {
        public Task<EBuyResponse<ImageResponseDto>> UploadImage(IFormFile file); 
        public Task<EBuyResponse<List<ImageResponseDto>>>  UploadManyImage(List<IFormFile> files);
        public Task<EBuyResponse> Delete(Guid imgId);
        public Task<EBuyResponse<ImageResponseDto>> GetImage(Guid imageId);
        public Task<EBuyResponse<List<ImageResponseDto>>> GetAll();
    }
}
