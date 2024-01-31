using Microsoft.EntityFrameworkCore;

namespace Task_1.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Domain.Entity.Task> Tasks { get; set; }
    }
}