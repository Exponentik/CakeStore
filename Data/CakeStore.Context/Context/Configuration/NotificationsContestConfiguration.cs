using CakeStore.Context.Entities;
using Microsoft.EntityFrameworkCore;


namespace CakeStore.Context;

public static class NotificationsContestConfiguration
{
    public static void ConfigureNotifications(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>().ToTable("notifications");
        modelBuilder.Entity<Notification>().Property(x => x.Message).IsRequired();
        modelBuilder.Entity<Notification>().Property(x => x.Title).HasMaxLength(1000);
        modelBuilder.Entity<Notification>().Property(x => x.DateCreated).IsRequired();
        //modelBuilder.Entity<Notification>().HasOne(x => x.User).WithMany(x => x.Notifications);

    }
}
