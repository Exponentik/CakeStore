using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;
using static System.Net.Mime.MediaTypeNames;

namespace CakeStore.Services.Images;

public class ImageModel
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public byte[] ImageData { get; set; }
}


//public class ImageModelProfile : Profile
//{
//    public ImageModelProfile()
//    {
//        CreateMap<Image, ImageModel>()
//            .BeforeMap<ImageModelActions>()
//            .ForMember(dest => dest.Id, opt => opt.Ignore())
//            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
//            ;
//    }

//    public class ImageModelActions : IMappingAction<Image, ImageModel>
//    {
//        private readonly IDbContextFactory<MainDbContext> contextFactory;

//        public ImageModelActions(IDbContextFactory<MainDbContext> contextFactory)
//        {
//            this.contextFactory = contextFactory;
//        }

//        public void Process(Image source, ImageModel destination, ResolutionContext context)
//        {
//            using var db = contextFactory.CreateDbContext();

            

//            var image = db.Images.Include(x => x.Product).FirstOrDefault(x => x.Id == source.Id);

//            destination.Id = image.Uid;
//            destination.ProductId = image.Product.Id;
//        }
//    }
//}