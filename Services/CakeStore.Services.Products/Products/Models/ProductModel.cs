using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;
using Microsoft.AspNetCore.Http;

namespace CakeStore.Services.Products;

public class ProductModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public IEnumerable<string> Categories { get; set; }
    public IEnumerable<Image> Images { get; set; }


}


public class ProductModelProfile : Profile
{
    public ProductModelProfile()
    {
        CreateMap<Product, ProductModel>()
            .BeforeMap<ProductModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            ;
    }

    public class ProductModelActions : IMappingAction<Product, ProductModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public ProductModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Product source, ProductModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            

            var product = db.Products./*Include(x => x.User).*/FirstOrDefault(x => x.Id == source.Id);

            destination.Id = product.Uid;
            destination.UserId = product.User.Id;
            destination.Categories = product.Categories?.Select(x => x.Title);
        }
    }
}