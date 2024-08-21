
using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CakeStore.Context;

public static class LikesContextConfiguration
{
    public static void ConfigureLikes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Like>().ToTable("likes");
        modelBuilder.Entity<Like>().Property(x=>x.LikeDate).IsRequired();
        modelBuilder.Entity<Like>().HasOne(x => x.Product).WithMany(x => x.Likes);
        //modelBuilder.Entity<Like>().HasOne(x => x.User).WithMany(x => x.Likes);
    }
    
}
