using Microsoft.EntityFrameworkCore;
using api.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using api.Models;

namespace api.data
{
    public class AplicationDBContext : IdentityDbContext<UserModel>
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {
            
    }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}