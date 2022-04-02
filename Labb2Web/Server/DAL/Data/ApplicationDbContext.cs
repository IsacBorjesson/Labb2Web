using Labb2Web.Server.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labb2Web.Server.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
