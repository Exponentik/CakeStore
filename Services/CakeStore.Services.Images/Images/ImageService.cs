using CakeStore.Context;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using AutoMapper;
using System;
using CakeStore.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using CakeStore.Common.Validator;
using Microsoft.EntityFrameworkCore.Internal;
using static System.Collections.Specialized.BitVector32;
using CakeStore.Services.Actions;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using static System.Net.Mime.MediaTypeNames;


namespace CakeStore.Services.Images;

public class ImageService : IImageService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ImageService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper, IModelValidator<CreateModel> createModelValidator,
        IAction action, IModelValidator<UpdateModel> updateModelValidator, IHttpContextAccessor _httpContextAccessor/*, UserManager<IdentityUser> _userManager*/)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.action = action;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
        this._httpContextAccessor = _httpContextAccessor;
        //this._userManager = _userManager;
    }
    public async Task<IEnumerable<ImageModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var like = await context.Images

            .Include(x => x.Product).ToListAsync();

        var result = like.Select(x => new ImageModel()
        {
            Id = x.Uid,
            ProductId = x.Product.Uid,
            ImageData = x.ImageData,
        });
        return result;
    }
    public async Task<ImageModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var image = await context.Images
            //.Include(x => x.Reviews)
            //.Include(x => x.Categories)
            //.Include(x => x.Images)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Uid == id);


        var result = new ImageModel()
        { 
            Id = image.Uid,
            ProductId = image.Product.Uid,
            ImageData = image.ImageData,
        };
        return result;
    }
    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.Where(x => x.Uid == id).FirstOrDefaultAsync();

       // category.Title = model.Title;


        context.Categories.Update(category);

        await context.SaveChangesAsync();
    }

        public async Task<ImageModel> GetByProductIdAndUserId(Guid productId, Guid userId)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var image = await context.Images

                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Product.Uid == productId);

            if (image == null) return null; // Если лайк не найден, возвращаем null

            var result = new ImageModel()
            {
                Id = image.Uid,
                ImageData = image.ImageData,
                ProductId = image.Product.Uid,
            };
            return result;
        }
        public async Task<ImageModel> Create(CreateModel model)
        {
            // await createModelValidator.CheckAsync(model);

            using var context = await dbContextFactory.CreateDbContextAsync();
            ICollection<Category> categories = new Collection<Category>();
            var usGuid = _httpContextAccessor.HttpContext.User.Identity.GetUserId();
            byte[] ImD;
        using (var memoryStream = new MemoryStream())
        {
            await model.ImageData.CopyToAsync(memoryStream);
            ImD= memoryStream.ToArray();
        }
        var image = new Context.Entities.Image()
            {
                Product = context.Products.FirstOrDefault(x => x.Uid == model.ProductId),
                ImageData = ImD

            };

            //mapper.Map<Product>(model);

            await context.Images.AddAsync(image);

            await context.SaveChangesAsync();

            //await action.PublicateBook(new PublicateBookModel()
            //{
            //    Id = like.Id,
            //    Title = like.Name,
            //    Description = like.Description
            //});
            var result = new ImageModel()
            {
                Id = image.Uid,
                ImageData = image.ImageData,
                ProductId = image.Product.Uid
            };
            return result;
        }
        public async Task Delete(Guid id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var image = await context.Images.Where(x => x.Uid == id).FirstOrDefaultAsync();

            if (image == null)
                throw new ProcessException($"Product (ID = {id}) not found.");

            context.Images.Remove(image);

            await context.SaveChangesAsync();
        }
    }
