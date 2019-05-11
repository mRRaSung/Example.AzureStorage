using Architecture.Models;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        //public DbSet<Category> Category { get; set; }
        //public DbSet<Product> Product { get; set; }
        //public DbSet<Blog> Blog { get; set; }
        //public DbSet<Post> Post { get; set; }
        //public DbSet<User> User { get; set; }
        //public DbSet<Group> Group { get; set; }

        //public DbSet<Animal> Animals { get; set; }
        //public DbSet<Cat> Cat { get; set; }
        //public DbSet<Dog> Dog { get; set; }
    }
}
