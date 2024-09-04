
using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CakeStore.Context;

public static class ProductContextConfiguration
{
    public static void ConfigureProducts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(1000);
        modelBuilder.Entity<Product>().HasMany(x => x.Categories).WithMany(x => x.Products).UsingEntity(t => t.ToTable("products_categories"));
        modelBuilder.Entity<Product>().HasMany(x => x.Likes).WithOne(x => x.Product);
        modelBuilder.Entity<Product>().HasMany(x => x.Reviews).WithOne(x => x.Product);
        modelBuilder.Entity<Product>().HasOne(x => x.User).WithMany(x => x.Products);
    }
}
