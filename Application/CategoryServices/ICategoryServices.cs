using Domain.Dtos;
using Shared.Common;

namespace Application.CategoryServices
{
    public interface ICategoryServices
    {
        public Task<EBuyResponse> Create(CategoryRequestDto request);
        public Task<EBuyResponse> Update(CategoryRequestDto request);
        public Task<EBuyResponse<CategoryResponseDto>> Get(Guid id);
        public Task<EBuyResponse<Paginated<CategoryResponseDto>>> GetAll(string search, int page, int pageSize);
        public Task<EBuyResponse> Delete(Guid id);
        public Task<EBuyResponse> Archive(Guid id);
    }
}
