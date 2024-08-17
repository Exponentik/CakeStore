using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CakeStore.Context.Entities;

public class Notification : BaseEntity
{
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime DateCreated { get; set; }

    public DateTime? DateRead { get; set; }

    public bool IsRead { get; set; } = false;

    public int UserId { get; set; }

    public virtual User User { get; set; }  // Связь с таблицей пользователей
}
