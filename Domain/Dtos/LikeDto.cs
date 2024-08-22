using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class RequesLikeDto
    {
        public Guid? productId { get; set; }
        public Guid? UserId { get; set; }
        public Guid Id { get; set; }
    }
}
