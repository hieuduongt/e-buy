using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Image = Domain.Entities.Image;


namespace Application.ImageService
{
    public class ImageService : IImageService
    {
        private readonly AppDBContext _dbContext;
        private readonly IImageService _imageServices;

        public ImageService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly string allowAtension = @"(?i)\.(png|jpg|jpeg|avif|webp|[^\.]+)$";

        public async Task<EBuyResponse<ImageResponseDto>> UploadImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                Regex reg = new Regex(allowAtension);
                MatchCollection fileExtensionMatcher = reg.Matches(file.FileName);
                string extension = fileExtensionMatcher[0].Value;
                Guid imageId = Guid.NewGuid();
                var filePath = Path.GetFullPath(Path.Combine($"{Environment.CurrentDirectory}/../Shared/Images"));
                string fullPath = $"/Shared/Images/{imageId}{extension}";
                var stream = System.IO.File.Create($"{filePath}/{imageId}{extension}");
                var newImage = new ImageResponseDto
                {
                    Id = imageId,
                    Url = fullPath,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsArchived = false,
                    InUse = false,
                };
                await file.CopyToAsync(stream);
                stream.Close();
                var Img = new Image
                    {
                    Id = newImage.Id,
                    InUse = newImage.InUse,
                    IsArchived = newImage.IsArchived,
                    IsDeleted   = newImage.IsDeleted,
                    CreatedDate = newImage.CreatedDate,
                    UpdatedDate = newImage.UpdatedDate,
                    Url = newImage.Url,
                    ProductId = null
                };

                await _dbContext.Images.AddAsync( Img);
                await _dbContext.SaveChangesAsync();
                return EBuyResponse<ImageResponseDto>.Success(newImage);
            }
            return EBuyResponse<ImageResponseDto>.Failed(new List<string> { "File ảnh không hợp lệ!"});

        }

        public async Task<EBuyResponse<List<ImageResponseDto>>> UploadManyImage(List<IFormFile> files)
        {
            List<ImageResponseDto> uploadedImages = new List<ImageResponseDto>();
            if (files.Count > 0)
            {
                try
                {
                    foreach (var file in files)
                    {
                        Guid imageId = Guid.NewGuid();
                        Regex reg = new Regex(allowAtension);
                        MatchCollection fileExtensionMatcher = reg.Matches(file.FileName);
                        string extension = fileExtensionMatcher[0].Value;
                        var filePath = Path.GetFullPath(Path.Combine($"{Environment.CurrentDirectory}/../Shared/Images"));
                        string fullPath = $"/Images/{imageId}{extension}";
                        var stream = System.IO.File.Create($"{filePath}/{imageId}{extension}");

                        await file.CopyToAsync(stream);
                        var image = new ImageResponseDto
                        {
                            Id = imageId,
                            Url = fullPath
                        };
                        stream.Close();
                        uploadedImages.Add(image);
                    }

                    var newImages = uploadedImages.Select(ui => new Image
                    {
                        Id = ui.Id,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        InUse = false,
                        IsArchived = false,
                        Url = ui.Url,
                        ProductId= null
                    }).ToList();
                    await _dbContext.Images.AddRangeAsync(newImages);
                    await _dbContext.SaveChangesAsync();
                    return EBuyResponse<List<ImageResponseDto>>.Success(uploadedImages);

                } catch (Exception ex)
                {
                    return EBuyResponse<List<ImageResponseDto>>.Failed(new List<string> { "File ảnh không hợp lệ!" });
                }

            }
            return EBuyResponse<List<ImageResponseDto>>.Failed(new List<string> { "File ảnh không hợp lệ!" });

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

            return EBuyResponse<ImageResponseDto>.Success(result);
        }
        public async Task<EBuyResponse<List<ImageResponseDto>>> GetAll()
        {
            var results = await _dbContext.Images.Where(i => !i.IsDeleted && !i.IsArchived).Select(i => new ImageResponseDto
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
    }
}
