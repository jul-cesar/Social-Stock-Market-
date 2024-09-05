
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
