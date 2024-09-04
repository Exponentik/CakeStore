namespace CakeStore.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    //public virtual ICollection<Like> Likes { get; set; }
    //public virtual ICollection<Order> Orders { get; set; }
    //public virtual ICollection<Review> Reviews { get; set; }
    //public virtual ICollection<Notification> Notifications { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
    public UserStatus Status { get; set; }
}
