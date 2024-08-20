using Domain.Dtos;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LikeServices
{
    public interface ILikeService
    {
        public Task<EBuyResponse> CreateLike(RequesLikeDto requesLikeDto);
        public Task<EBuyResponse> Delete(Guid productId);
    }
}
