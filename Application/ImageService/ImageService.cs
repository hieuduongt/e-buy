using Application.CategoryServices;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Common;

namespace E_BUY.ImageService
{
    public class ImageService : IImageService
    {
        private readonly AppDBContext _dbContext;
        private readonly ICategoryServices _imageServices;
        public ImageService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EBuyResponse<List<ImageResponseDto>>> GetAll()
        {
            var results = await _dbContext.Images.Where(i=>!i.IsDeleted && !i.IsArchived).Select(i => new ImageResponseDto
            {
                Id = i.Id,
                Url = i.Url,
                ProductId = i.ProductId,
                IsArchived = i.IsArchived,
                IsDeleted = i.IsDeleted,
                UpdatedDate = i.UpdatedDate,
                CreatedDate = i.CreatedDate,
            }).ToListAsync();
            if (results.Count == 0)
            {
                return EBuyResponse<List<ImageResponseDto>>.Failed(new List<string> { "Không tìm thấy ảnh !" });
            }

            return EBuyResponse<List<ImageResponseDto>>.Success(results);
        }
        public async Task<EBuyResponse<ImageResponseDto>> GetImage(Guid imageId)
        {
            var result = await _dbContext.Images.Where(i => i.Id == imageId && !i.IsDeleted && !i.IsArchived).Select(i => new ImageResponseDto
            {
                Id = i.Id,
                Url = i.Url,
                ProductId = i.ProductId,
                IsArchived = i.IsArchived,
                IsDeleted = i.IsDeleted,
                UpdatedDate = i.UpdatedDate,
                CreatedDate = i.CreatedDate,
            }).FirstOrDefaultAsync();
            if (result == null)
            {
                return EBuyResponse<ImageResponseDto>.Failed(new List<string> { "Không tìm thấy ảnh !" });
            }

            return   EBuyResponse<ImageResponseDto>.Success(result);
        }

        public async Task<EBuyResponse<List<ImageResponseDto>>> Get(Guid productId)
        {
            var result = await _dbContext.Images.Where(i => i.ProductId == productId && !i.IsDeleted && !i.IsArchived).Select(i => new ImageResponseDto
            {
                Id = i.Id,
                Url = i.Url,
                ProductId = i.ProductId,
                IsArchived = i.IsArchived,
                IsDeleted = i.IsDeleted,
                UpdatedDate = i.UpdatedDate,
                CreatedDate = i.CreatedDate,
            }).ToListAsync();
            if (result.Count == 0)
            {
                return EBuyResponse<List<ImageResponseDto>>.Failed(new List<string> { "Không tìm thấy ảnh !" });
            }

            return EBuyResponse<List<ImageResponseDto>>.Success(result);
        }
        public async Task<EBuyResponse> Create(ImageRequestDto imageRequestDto)
        {
            var resultImage = new Image
            {
                Id = Guid.NewGuid(),
                Url = imageRequestDto.Url,
                ProductId = imageRequestDto.ProductId,
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                IsArchived = false
            };
            _dbContext.Images.AddAsync(resultImage);
            await _dbContext.SaveChangesAsync();

            return EBuyResponse.Success();
        }
        public async Task<EBuyResponse> Delete(Guid imgId)
        {
            var Image = _dbContext.Images.FirstOrDefault(x => x.Id == imgId);
            if (Image == null || Image.IsDeleted || Image.IsArchived)
            {
                return EBuyResponse.Failed(new List<string> { "không tìm thấy ảnh !" });
            }
            Image.IsDeleted = true;
            Image.UpdatedDate = DateTime.UtcNow;

            _dbContext.Images.Update(Image);
            await _dbContext.SaveChangesAsync();

            return EBuyResponse.Success();
        }

    }
}
