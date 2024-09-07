using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;

namespace CakeStore.Services.Likes;

public class LikeModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }
}


public class ProductModelProfile : Profile
{
    public ProductModelProfile()
    {
        CreateMap<Like, LikeModel>()
            .BeforeMap<LikeModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            ;
    }

    public class LikeModelActions : IMappingAction<Like, LikeModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public LikeModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Like source, LikeModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            

            var like = db.Likes./*Include(x => x.User).*/FirstOrDefault(x => x.Id == source.Id);

            destination.Id = like.Uid;
            destination.UserId = like.User.Id;
        }
    }
}