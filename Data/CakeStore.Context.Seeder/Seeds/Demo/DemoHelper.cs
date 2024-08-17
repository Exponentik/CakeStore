namespace CakeStore.Context.Seeder;

using CakeStore.Context.Entities;

public class DemoHelper
{
    public IEnumerable<Product> GetProducts = new List<Product>
    {
        new Product()
        {
            Uid = Guid.NewGuid(),
            Name = "Napoleon",
            Description = "Krutoi tort",

            Categories = new List<Category>()
            {
                new Category()
                {
                    Title = "S kremom",
                },
                new Category()
                {
                    Title = "Bomba",
                }
            },
            User = new User()
            {
                Uid = Guid.NewGuid(),
                Name = "Petya",
                Email = "",
                Password = "",
                UserName = ""

        }
    }
    };
        }
