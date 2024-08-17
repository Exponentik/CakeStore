using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CakeStore.Context.Context.Configuration;

public static class OrdersContextConfiguration
{
    public static void ConfigureOrders(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable("orders");
    }
}
