using Microsoft.EntityFrameworkCore;
namespace TestAPI.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FileItem> FileItems { get; set; } = null;
        public DbSet<Result> Results { get; set; } = null;
        public DbSet<Value> Values { get; set; } = null;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            Database.EnsureCreated();
        }

    }
}
