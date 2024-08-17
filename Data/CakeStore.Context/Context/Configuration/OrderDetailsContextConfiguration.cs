using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeStore.Context.Context.Configuration;

public static class OrderDetailsContextConfiguration
{
    public static void ConfigureOrderDetails(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>().ToTable("order_detail");
        modelBuilder.Entity<OrderDetail>().Property(x => x.Quantitiy).IsRequired();
        modelBuilder.Entity<OrderDetail>().Property(x => x.UnitPrice).IsRequired();
        modelBuilder.Entity<OrderDetail>().HasOne(x => x.Product).WithMany(x => x.OrderDetails);
        modelBuilder.Entity<OrderDetail>().HasOne(x => x.Order).WithMany(x => x.OrderDetails);
    }
}
