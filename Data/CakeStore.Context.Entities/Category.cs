namespace CakeStore.Context.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
