using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDBContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
           .HasMany(u => u.Roles)
           .WithMany(r => r.Users)
           .UsingEntity<IdentityUserRole<Guid>>(
               ur => ur.HasOne<Role>().WithMany().HasForeignKey(ur => ur.RoleId),
               ur => ur.HasOne<User>().WithMany().HasForeignKey(ur => ur.UserId),
               ur =>
               {
                   ur.ToTable("AspNetUserRoles");
               });

            Guid ADMIN_ID = Guid.Parse("570431fa-36ab-45bc-b135-3f6060be55e0");
            Guid ADMIN_ROLE_ID = Guid.Parse("86b40157-9c95-41a1-aac6-b388026b6193");
            Guid MANAGER_ROLE_ID = Guid.Parse("153fd8a7-d3f6-4e85-936c-e964f69b98c6");
            Guid USER_ROLE_ID = Guid.Parse("55c6b873-1ef7-413f-9c92-2e44582558a1");
            Guid GUEST_ROLE_ID = Guid.Parse("152aa6d6-30f3-408f-9dca-be6bbd66c62b");

            var hasher = new PasswordHasher<User>();
            _ = builder.Entity<User>().HasData(new User
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@ebuy.com",
                NormalizedEmail = "admin@ebuy.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin123@"),
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                LastActiveDate = DateTime.UtcNow
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = ADMIN_ROLE_ID,
                Name = "admin",
                NormalizedName = "admin"
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = MANAGER_ROLE_ID,
                Name = "manager",
                NormalizedName = "manager"
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = USER_ROLE_ID,
                Name = "user",
                NormalizedName = "user"
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = GUEST_ROLE_ID,
                Name = "guest",
                NormalizedName = "guest"
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Like> Likes { get; set; }


    }
}
