namespace CakeStore.Context.Entities;

public class Order : BaseEntity
{
    //public virtual User User { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; private set; }
}
