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


namespace CakeStore.Services.Likes;

public class LikeService : ILikeService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public LikeService(IDbContextFactory<MainDbContext> dbContextFactory,IMapper mapper, IModelValidator<CreateModel> createModelValidator,
        IAction action, IHttpContextAccessor _httpContextAccessor/*, UserManager<IdentityUser> _userManager*/) 
    { 
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.action = action;
        this.createModelValidator = createModelValidator;
        this._httpContextAccessor = _httpContextAccessor;
        //this._userManager = _userManager;
    }
    public async Task<IEnumerable<LikeModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var like = await context.Likes

            .Include(x => x.User).Include(x=>x.Product).ToListAsync();

        var result = like.Select(x => new LikeModel()
        {
            Id = x.Uid,
            UserId = x.User.Id,
            ProductId = x.Product.Uid,
        });
        return result;
    }
    public async Task<LikeModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var like = await context.Likes
            //.Include(x => x.Reviews)
            //.Include(x => x.Categories)
            //.Include(x => x.Images)
            .Include(x => x.User)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Uid == id);


        var result = new LikeModel()
        {
            Id = like.Uid,
            UserId = like.User.Id,
            ProductId = like.Product.Uid,
        };
        return result;
    }
    public async Task<LikeModel> GetByUserId(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var like = await context.Likes
            //.Include(x => x.Reviews)
            //.Include(x => x.Categories)
            //.Include(x => x.Images)
            .Include(x => x.User)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.User.Id == id);


        var result = new LikeModel()
        {
            Id = like.Uid,
            UserId = like.User.Id,
            ProductId = like.Product.Uid,
        };
        return result;
    }
    public async Task<LikeModel> GetByProductIdAndUserId(Guid productId, Guid userId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var like = await context.Likes
            .Include(x => x.User)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Product.Uid == productId && x.User.Id == userId);

        if (like == null) return null; // Если лайк не найден, возвращаем null

        var result = new LikeModel()
        {
            Id = like.Uid,
            UserId = like.User.Id,
            ProductId = like.Product.Uid,
        };
        return result;
    }
    public async Task<LikeModel> Create(CreateModel model)
    {
       // await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();
        ICollection<Category> categories = new Collection<Category>();
        var usGuid = _httpContextAccessor.HttpContext.User.Identity.GetUserId();
        var like = new Like()
        {

            User = context.Users.FirstOrDefault(x => x.Id == model.UserId),
            Product = context.Products.FirstOrDefault(x => x.Uid == model.ProductId)

        };
        
            //mapper.Map<Product>(model);

        await context.Likes.AddAsync(like);

        await context.SaveChangesAsync();

        //await action.PublicateBook(new PublicateBookModel()
        //{
        //    Id = like.Id,
        //    Title = like.Name,
        //    Description = like.Description
        //});
        var result = new LikeModel()
        {
            Id = like.Uid,
            UserId = like.User.Id,
            ProductId = like.Product.Uid
        };
        return result;
    }
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var like = await context.Likes.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (like == null)
            throw new ProcessException($"Product (ID = {id}) not found.");

        context.Likes.Remove(like);

        await context.SaveChangesAsync();
    }
}
