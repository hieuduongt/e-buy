using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastActiveDate { get; set; }
        public string IsEditBy { get; set; } = string.Empty;
        public string ConnectionId { get; set; } = string.Empty;
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Order>? Orders { get; set; }
    }
}
