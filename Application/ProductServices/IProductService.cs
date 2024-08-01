using Domain.Dtos;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductServices
{
    public interface IProductService
    {
        public Task<EBuyResponse<Paginated<ProductResponseDto>>> GetAll(string search, int page, int pageSize);
        public Task<EBuyResponse<ProductResponseDto>>Get(Guid productId);
        public Task<EBuyResponse>Create(ProductRequesDto productRequesDto);
        public Task<EBuyResponse> Update(ProductRequesDto request);
        public Task<EBuyResponse> Delete(Guid productId);
        public Task<EBuyResponse> Archive(Guid productId);
    }
}
