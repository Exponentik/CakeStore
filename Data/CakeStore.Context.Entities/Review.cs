using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeStore.Context.Entities;

public class Review : BaseEntity
{
    public string ProductId { get; set; }
    public string UserId { get; set; }
    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
    public string Comment { get; set; }
}
