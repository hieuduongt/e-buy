using Azure.Core;
using Domain.Dtos;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Application.LikeServices
{
    public class LikeService : ILikeService
    {
        public readonly AppDBContext _dbContext;
        public LikeService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EBuyResponse> CreateLike(RequesLikeDto requesLikeDto)
        {
            if (requesLikeDto == null)
            {
                return EBuyResponse.Failed(new List<string> { "Thiếu thông tin!" });
            }
            var newLike = new Like
            {
                Id = requesLikeDto.Id, 
                IsDeleted = false,
                UserId = requesLikeDto.UserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                productId = requesLikeDto.productId,

            };
            _dbContext.Likes.AddAsync(newLike);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();
        }

        public async Task<EBuyResponse> Delete(Guid productId)
        {
            var result =await _dbContext.Likes.FirstOrDefaultAsync(l => l.productId == productId);

            if(result == null)
            {
                return EBuyResponse.Failed(new List<string> { "Không tìm thấy sản phẩm này!" });
            }
            result.IsDeleted = true;
            result.UpdatedDate = DateTime.UtcNow;
            _dbContext.Likes.Update(result);
            await _dbContext.SaveChangesAsync();
            return EBuyResponse.Success();

        }


    }
}
