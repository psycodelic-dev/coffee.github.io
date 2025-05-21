using Microsoft.EntityFrameworkCore;
using MelikeCoffee.Models;

namespace MelikeCoffee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Hot Coffee", ImageUrl = "/images/latte.jpg" },
                new Category { CategoryId = 2, Name = "Cold Coffee", ImageUrl = "/images/cold-brew.jpg" },
                new Category { CategoryId = 3, Name = "Whole Bean", ImageUrl = "/images/medium.jpg" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Category = "Hot Coffee", ProductName = "Espresso", Size = "Medium", Price = 2, ImageUrl = "/images/espresso.jpg" },
                new Product { ProductId = 2, Category = "Hot Coffee", ProductName = "Caffè Americano", Size = "Medium", Price = 2, ImageUrl = "/images/americano.jpg" },
                new Product { ProductId = 3, Category = "Hot Coffee", ProductName = "Cappuccino", Size = "Medium", Price = 4, ImageUrl = "/images/cappuccino.jpg" },
                new Product { ProductId = 4, Category = "Hot Coffee", ProductName = "Caramel Macchiato", Size = "Medium", Price = 5, ImageUrl = "/images/caramel-macchiato.jpg" },
                new Product { ProductId = 5, Category = "Hot Coffee", ProductName = "Caffè Mocha", Size = "Medium", Price = 5, ImageUrl = "/images/mocha.jpg" },
                new Product { ProductId = 6, Category = "Hot Coffee", ProductName = "Filter Coffee", Size = "Medium", Price = 2, ImageUrl = "/images/filter-coffee.jpg" },
                new Product { ProductId = 7, Category = "Hot Coffee", ProductName = "White Chocolate Mocha", Size = "Medium", Price = 5, ImageUrl = "/images/white-chocolate-mocha.jpg" },
                new Product { ProductId = 8, Category = "Hot Coffee", ProductName = "Caffè Latte", Size = "Medium", Price = 4, ImageUrl = "/images/latte.jpg" },
                
                new Product { ProductId = 9, Category = "Cold Coffee", ProductName = "Cold Brew", Size = "Medium", Price = 3, ImageUrl = "/images/cold-brew.jpg" },
                new Product { ProductId = 10, Category = "Cold Coffee", ProductName = "Iced Espresso", Size = "Medium", Price = 2, ImageUrl = "/images/iced-espresso.jpg" },
                new Product { ProductId = 11, Category = "Cold Coffee", ProductName = "Iced Caffè Latte", Size = "Medium", Price = 4, ImageUrl = "/images/iced-latte.jpg" },
                new Product { ProductId = 12, Category = "Cold Coffee", ProductName = "Iced Caramel Macchiato", Size = "Medium", Price = 5, ImageUrl = "/images/iced-macchiato.jpg" },
                new Product { ProductId = 13, Category = "Cold Coffee", ProductName = "Iced Caffè Mocha", Size = "Medium", Price = 5, ImageUrl = "/images/iced-mocha.jpg" },

                new Product { ProductId = 14, Category = "Whole Bean", ProductName = "Light Roast", Size = "500g", Price = 20, ImageUrl = "/images/light.jpg" },
                new Product { ProductId = 15, Category = "Whole Bean", ProductName = "Medium Roast", Size = "500g", Price = 20, ImageUrl = "/images/medium.jpg" },
                new Product { ProductId = 16, Category = "Whole Bean", ProductName = "Dark Roast", Size = "500g", Price = 20, ImageUrl = "/images/dark.jpg" }


            );
        }

    }
}
