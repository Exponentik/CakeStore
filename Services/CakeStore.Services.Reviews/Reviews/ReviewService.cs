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


namespace CakeStore.Services.Reviews;

public class ReviewService : IReviewService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;
    private readonly IModelValidator<UpdateModel> updateModelValidator;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ReviewService(IDbContextFactory<MainDbContext> dbContextFactory,IMapper mapper, IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator, IAction action, IHttpContextAccessor _httpContextAccessor/*, UserManager<IdentityUser> _userManager*/) 
    { 
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.action = action;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
        this._httpContextAccessor = _httpContextAccessor;
        //this._userManager = _userManager;
    }
    public async Task<IEnumerable<ReviewModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var reviews = await context.Reviews

            .Include(x => x.User).Include(x=>x.Product).ToListAsync();

        var result = reviews.Select(x => new ReviewModel()
        {
            Id = x.Uid,
            UserId = x.User.Id,
            ProductId = x.Product.Uid,
            Comment = x.Comment,
        });
        return result;
    }
    public async Task<ReviewModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews
            //.Include(x => x.Reviews)
            .Include(x => x.Product)
            //.Include(x => x.Images)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Uid == id);


        var result = new ReviewModel()
        {
            Id = review.Uid,
            UserId = review.User.Id,
            ProductId = review.Product.Uid,
            Comment = review.Comment,
        };
        return result;
    }      
    public async Task<ReviewModel> Create(CreateModel model)
    {
       // await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();
        ICollection<Category> categories = new Collection<Category>();
        
        var usGuid = _httpContextAccessor.HttpContext.User.Identity.GetUserId();
        var review = new Review()
        {
            Comment = model.Comment,
            User = context.Users.FirstOrDefault(x => x.Id == model.UserId),
            Product = context.Products.FirstOrDefault(x => x.Uid == model.ProductId)

        };
        
            //mapper.Map<Product>(model);

        await context.Reviews.AddAsync(review);

        await context.SaveChangesAsync();

        //await action.PublicateBook(new PublicateBookModel()
        //{
        //    Id = review.Id,
        //    Title = review.Name,
        //    Description = review.Description
        //});
        var result = new ReviewModel()
        {
            Id = review.Uid,
            UserId = review.User.Id,
            Comment = review.Comment,
        };
        return result;
    }
    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews.Where(x => x.Uid == id).FirstOrDefaultAsync();

        review.Comment = model.Comment;

        context.Reviews.Update(review);

        await context.SaveChangesAsync();

    }
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (review == null)
            throw new ProcessException($"Product (ID = {id}) not found.");

        context.Reviews.Remove(review);

        await context.SaveChangesAsync();
    }
}
