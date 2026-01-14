using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Models.Models;


namespace OnlineShopping.Data
{
    public class ApplicationDbcontext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set;}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }

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
                    ISBN = "ELEC-001",
                    CategoryId = 1,
                    ImageUrl= ""

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
                    ISBN = "BOOK-001",
                    CategoryId = 2,
                    ImageUrl = ""
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
                    CategoryId = 3,
                    ImageUrl = ""
                }
                );

            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = 2,
                Name = "TechCorp",
                Address = "123 Tech Street",
                City = "Techville",
                PostalCode = "90001",
                PhoneNumber = 123-456-7890
            },
            new Company
            {
                Id = 3,
                Name = "BookWorld",
                Address = "456 Book Avenue",
                City = "Readertown",
                PostalCode = "90002",
                PhoneNumber = 234-567-8901
            }
            );
        }
    }
}


