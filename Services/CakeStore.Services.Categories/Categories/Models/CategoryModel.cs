using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;

namespace CakeStore.Services.Categories;

public class CategoryModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}


