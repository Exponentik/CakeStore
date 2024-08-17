namespace CakeStore.Context.Entities;

public class OrderDetail : BaseEntity
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantitiy { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}
