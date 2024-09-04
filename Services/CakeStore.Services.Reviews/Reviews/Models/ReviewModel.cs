using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;

namespace CakeStore.Services.Reviews;

public class ReviewModel
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid UserId { get; set; }

    public string Comment { get; set; }

}


public class ProductModelProfile : Profile
{
    public ProductModelProfile()
    {
        CreateMap<Product, ReviewModel>()
            .BeforeMap<ProductModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            ;
    }

    public class ProductModelActions : IMappingAction<Product, ReviewModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public ProductModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Product source, ReviewModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            

            var product = db.Products./*Include(x => x.User).*/FirstOrDefault(x => x.Id == source.Id);

            destination.Id = product.Uid;
            destination.UserId = product.User.Id;
        }
    }
}