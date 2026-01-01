using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;


namespace OnlineShopping.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Books", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Clothing", DisplayOrder = 3 }
            );
        }
    }
}
