
using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CakeStore.Context;

public static class ImagesContextConfiguration
{
    public static void ConfigureImages(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>().ToTable("images");
        modelBuilder.Entity<Image>().HasOne(x => x.Product).WithMany(x => x.Images);
        //modelBuilder.Entity<Like>().HasOne(x => x.User).WithMany(x => x.Likes);
    }
    
}
