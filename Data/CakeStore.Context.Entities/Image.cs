using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CakeStore.Context.Entities;

public class Image : BaseEntity
{
    [JsonIgnore]
    public virtual Product Product { get; set; }
    public byte[] ImageData { get; set; }
}
