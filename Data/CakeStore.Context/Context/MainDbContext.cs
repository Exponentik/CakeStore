namespace CakeStore.Context;

using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CakeStore.Context.Context.Configuration;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCategories();
        modelBuilder.ConfigureProducts();
        modelBuilder.ConfigureOrders();
        modelBuilder.ConfigureReviews();
        modelBuilder.ConfigureLikes();
        modelBuilder.ConfigureOrderDetails();
        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureNotifications();
    }
}
