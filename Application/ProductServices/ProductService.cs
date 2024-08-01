using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly AppDBContext _dbContext;

        public ProductService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<EBuyResponse> Archive(Guid productId)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (result == null || result.IsDeleted || result.IsArchived)
            {
                return EBuyResponse.Failed(new List<string> { "Không tìm thấy sản phẩm này!" });
            }
            result.IsArchived = true;
            result.UpdatedDate = DateTime.UtcNow;
            _dbContext.Products.Update(result);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();
        }

        public async Task<EBuyResponse> Create(ProductRequesDto productRequesDto)
        {
            if (productRequesDto == null)
            {
                return EBuyResponse.Failed(
                    new List<string> { "Invalid person data" }
                );
            }
            else
            {
                var Newproduct = new Product
                {
                    CategoryId = productRequesDto.CategoryId,
                    Id = productRequesDto.Id,
                    Name = productRequesDto.Name,
                    Description = productRequesDto.Description,
                    Quantity = productRequesDto.Quantity,
                    OriginalPrice = productRequesDto.OriginalPrice,
                    SalePrice = productRequesDto.SalePrice,
                    Images = productRequesDto.image,
                    SoldNumber = productRequesDto.SoldNumber,
                    Currency = productRequesDto.Currency,
                    CreatedDate = DateTime.UtcNow,
                    IsArchived = false,
                    IsDeleted = false,
                    UpdatedDate = DateTime.UtcNow
                };
                await _dbContext.Products.AddAsync(Newproduct);
                await _dbContext.SaveChangesAsync();
                return EBuyResponse.Success();
            }
        }

        public async Task<EBuyResponse> Delete(Guid productId)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(p=>p.Id == productId);
            if (result == null || result.IsDeleted || result.IsArchived)
            {
                return EBuyResponse.Failed(new List<string> { "Không tìm thấy sản phẩm này!" });
            }
            result.IsDeleted = true;
            result.UpdatedDate = DateTime.UtcNow;
             _dbContext.Products.Update(result);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();

        }

        public async Task<EBuyResponse> Update(ProductRequesDto request)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (result == null || result.IsDeleted || result.IsArchived)
            {
                return EBuyResponse.Failed(new List<string> { "Không tìm thấy sản phẩm này!" });
            }
            result.IsDeleted = false;
            result.IsArchived = false;
            result.SalePrice = request.SalePrice;
            result.UpdatedDate = DateTime.UtcNow;
            result.Name = request.Name;
            result.Description = request.Description;
            result.Images = result.Images;
            result.Currency = request.Currency;
            result.SalePrice = result.SalePrice;
            _dbContext.Update(result);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();

        }

        public async Task<EBuyResponse<ProductResponseDto>> Get(Guid productId)
        {
            var results = await _dbContext.Products.Where(p=>!p.IsDeleted && !p.IsArchived)
                .Where(p => p.Id == productId).Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    OriginalPrice = p.OriginalPrice,
                    SalePrice = p.SalePrice,
                    image = p.Images,
                    IsDeleted = p.IsDeleted,
                    IsArchived = p.IsArchived,
                    Currency = p.Currency,
                    SoldNumber = p.SoldNumber,
                    Category = p.Category,
                }).FirstOrDefaultAsync();
            if(results == null)
            {
                return EBuyResponse<ProductResponseDto>.Failed(new List<string> { "khong có sản phẩm nào trùng với id trên" });
            }
            return  EBuyResponse<ProductResponseDto>.Success(results);
        }

        public async Task<EBuyResponse<Paginated<ProductResponseDto>>> GetAll(string search, int page, int pageSize)
        {
            var resultsSeach = await _dbContext.Products.Where(p => !p.IsDeleted && !p.IsArchived)
                                            .Where(p => p.Name.ToLower().Contains(search.ToLower()) ||
                                                        p.Description.ToLower().Contains(search.ToLower()) ||
                                                        p.CreatedDate.ToString().ToLower().Equals(search.ToLower()) ||
                                                        p.Quantity.ToString().Contains(search.ToLower()) ||
                                                        (p.Category != null && p.Category.Name.ToLower().Contains(search.ToLower())) ||
                                                        p.UpdatedDate.ToString().ToLower().Equals(search.ToLower()) ||
                                                        p.Images.Any(i => i.Url.ToLower().Contains(search.ToLower())))
                                                        .Skip((page - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .Select(p => new ProductResponseDto
                                                        {
                                                            Id  = p.Id,
                                                            Name = p.Name,
                                                            Description = p.Description,
                                                            Quantity = p.Quantity,
                                                            CreatedDate = p.CreatedDate,
                                                            UpdatedDate = p.UpdatedDate,
                                                            OriginalPrice = p.OriginalPrice,
                                                            SalePrice = p.SalePrice,
                                                            image = p.Images,
                                                            IsDeleted = p.IsDeleted,
                                                            IsArchived = p.IsArchived,
                                                            Currency = p.Currency,
                                                            SoldNumber = p.SoldNumber,
                                                            Category = p.Category,
                                                        }).ToListAsync();

            if (resultsSeach.Count == 0)
            {
                return EBuyResponse<Paginated<ProductResponseDto>>.Failed(new List<string> { "Không tim thấy sản phẩm nào" });
            }
            else
            {
                var totalRecords = await GetNumberOfProductBySearchText(search.ToLower());

                return new EBuyResponse<Paginated<ProductResponseDto>>
                {
                    Code = 200,
                    Result = new Paginated<ProductResponseDto>
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalPages = (totalRecords + pageSize - 1) / pageSize,
                        Items = resultsSeach
                    },
                    IsSuccess = true
                };
            }
        }
        public async Task<int> GetNumberOfProductBySearchText(string search)
        {
            try
            {
                return await _dbContext.Products
                    .Where(c => !c.IsDeleted && !c.IsArchived)
                    .CountAsync(
                    p => p.Name.ToLower().Contains(search.ToLower()) ||
                         p.CreatedDate.ToString().ToLower().Equals(search.ToLower()) ||
                         p.Quantity.ToString().Contains(search.ToLower()) ||
                         (p.Category != null && p.Category.Name.ToLower().Contains(search.ToLower())) ||
                         p.UpdatedDate.ToString().ToLower().Equals(search.ToLower()) ||
                         p.Images.Any(i => i.Url.ToLower().Contains(search.ToLower()))
                );
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
