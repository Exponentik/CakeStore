namespace CakeStore.Context;

using CakeStore.Context.Context.Configuration;
using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class MainDbContext : DbContext
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
