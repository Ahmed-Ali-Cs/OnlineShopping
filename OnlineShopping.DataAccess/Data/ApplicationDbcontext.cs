using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Models.Models;


namespace OnlineShopping.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Books", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Clothing", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Smartphone",
                    Description = "Latest model smartphone with advanced features",
                    Price = 699.99,
                    ListPrice = 799.99,
                    Price50 = 649.99,
                    Price100 = 599.99,
                    Author = "TechCorp",
                    ISBN = "ELEC-001"


                },
                new Product
                {
                    Id = 2,
                    Title = "Science Fiction Novel",
                    Description = "A thrilling science fiction novel set in a dystopian future",
                    Price = 14.99,
                    ListPrice = 19.99,
                    Price50 = 12.99,
                    Price100 = 9.99,
                    Author = "John Doe",
                    ISBN = "BOOK-001"

                },
                new Product
                {
                    Id = 3,
                    Title = "Designer T-Shirt",
                    Description = "Comfortable and stylish designer t-shirt",
                    Price = 29.99,
                    ListPrice = 39.99,
                    Price50 = 24.99,
                    Price100 = 19.99,
                    Author = "FashionBrand",
                    ISBN = "CLOTH-001",
                }
                );
        }
    }
}


