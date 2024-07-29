using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Common;

namespace Application.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly AppDBContext _dbContext;
        public CategoryServices(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EBuyResponse> Archive(Guid id)
        {
            var result = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null || result.IsDeleted || result.IsArchived)
            {
                return EBuyResponse.Failed(new List<string> { "Không tìm thấy danh mục này!" });
            }
            result.IsArchived = true;
            result.UpdatedDate = DateTime.UtcNow;
            _dbContext.Categories.Update(result);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();
        }

        public async Task<EBuyResponse> Create(CategoryRequestDto request)
        {
            if (request == null
                || string.IsNullOrEmpty(request.Name)
                || string.IsNullOrEmpty(request.UrlPath)
                )
            {
                return EBuyResponse.Failed(new List<string> { "Thiếu thông tin!" });
            }
            var existed = await _dbContext.Categories.FirstOrDefaultAsync(
                c => c.Name.ToLower().Contains(request.Name.ToLower())
                || c.UrlPath.ToLower().Contains(request.UrlPath.ToLower()));
            if (existed != null)
            {
                return EBuyResponse.Failed(new List<string> { "Danh mục này đã chứa thông tin về tên hoặc URL bị trùng, vui lòng kiểm tra lại!" });
            }
            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UrlPath = request.UrlPath,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                IsArchived = false,
                IsDeleted = false,
                UpdatedDate = DateTime.UtcNow
            };
            await _dbContext.Categories.AddAsync(newCategory);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();
        }

        public async Task<EBuyResponse> Delete(Guid id)
        {
            var result = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null || result.IsDeleted || result.IsArchived)
            {
                return EBuyResponse.Failed(new List<string> { "Không tìm thấy danh mục này!" });
            }
            result.IsDeleted = true;
            result.UpdatedDate = DateTime.UtcNow;
            _dbContext.Categories.Update(result);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();
        }

        public async Task<EBuyResponse<CategoryResponseDto>> Get(Guid id)
        {
            var result = await _dbContext.Categories.Where(c => c.Id == id && !c.IsDeleted && !c.IsArchived).Select(
                c => new CategoryResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    UrlPath = c.UrlPath,
                    CreatedDate = c.CreatedDate,
                    IsArchived = c.IsArchived,
                    UpdatedDate = c.UpdatedDate,
                    IsDeleted = c.IsDeleted,
                })
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return EBuyResponse<CategoryResponseDto>.Failed(new List<string> { "Không tìm thấy danh mục này!" });
            }
            return EBuyResponse<CategoryResponseDto>.Success(result);
            throw new NotImplementedException();
        }

        public async Task<EBuyResponse<Paginated<CategoryResponseDto>>> GetAll(string search, int page, int pageSize)
        {
            var results = await _dbContext.Categories
                .Where(c => !c.IsDeleted && !c.IsArchived)
                .Where(
                c => c.Name.ToLower().Contains(search.ToLower())
                || c.Description.ToLower().Contains(search.ToLower())
                || c.UrlPath.ToLower().Contains(search.ToLower())
                )
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    UrlPath = c.UrlPath,
                    CreatedDate = c.CreatedDate,
                    IsArchived = c.IsArchived,
                    IsDeleted = c.IsDeleted,
                    UpdatedDate = c.UpdatedDate
                })
                .ToListAsync();
            if (results.Count == 0)
            {
                return EBuyResponse<Paginated<CategoryResponseDto>>.Failed(new List<string> { "Không tìm thấy danh mục nào!" });
            }
            var totalRecords = await GetNumberOfCategoriesBySearchText(search.ToLower());
            return new EBuyResponse<Paginated<CategoryResponseDto>>
            {
                Code = 200,
                IsSuccess = true,
                Result = new Paginated<CategoryResponseDto>
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = (totalRecords + pageSize - 1) / pageSize,
                    Items = results
                }
            };
        }

        public async Task<EBuyResponse> Update(CategoryRequestDto request)
        {
            if (request == null
                || string.IsNullOrEmpty(request.Name)
                || string.IsNullOrEmpty(request.UrlPath)
                )
            {
                return EBuyResponse.Failed(new List<string> { "Thiếu thông tin!" });
            }
            var result = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (result == null || result.IsDeleted || result.IsArchived)
            {
                return EBuyResponse.Failed(new List<string> { "Không tìm thấy danh mục này!" });
            }
            result.Name = request.Name;
            result.UrlPath = request.UrlPath;
            result.Description = request.Description;
            result.UpdatedDate = DateTime.UtcNow;
            _dbContext.Categories.Update(result);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();
        }

        public async Task<int> GetNumberOfCategoriesBySearchText(string searchText)
        {
            try
            {
                return await _dbContext.Categories
                    .Where(c => !c.IsDeleted && !c.IsArchived)
                    .CountAsync(
                    c => c.Name.ToLower().Contains(searchText)
                || c.Description.ToLower().Contains(searchText)
                || c.UrlPath.ToLower().Contains(searchText)
                );
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
