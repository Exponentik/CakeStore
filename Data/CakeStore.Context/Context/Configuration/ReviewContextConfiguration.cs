using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeStore.Context.Context.Configuration
{
    public static class ReviewContextConfiguration
    {
        public static void ConfigureReviews(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().ToTable("reviews");
            modelBuilder.Entity<Review>().Property(x => x.Comment).HasMaxLength(10000).IsRequired();
            modelBuilder.Entity<Review>().HasOne(x => x.Product).WithMany(x => x.Reviews);
        }
    }
}
