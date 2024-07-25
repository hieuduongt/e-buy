using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public List<User> Users { get; set; } = new List<User>();
    }
}
