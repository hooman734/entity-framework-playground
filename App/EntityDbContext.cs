using Microsoft.EntityFrameworkCore;

namespace App
{
    public class EntityDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Page> Pages { get; set; }
        
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}