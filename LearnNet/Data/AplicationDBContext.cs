using Microsoft.EntityFrameworkCore;
using api.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.data
{
    public class AplicationDBContext : IdentityDbContext<UserModel>
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {
            
    }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}