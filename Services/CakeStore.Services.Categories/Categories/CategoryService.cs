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


namespace CakeStore.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;
    private readonly IModelValidator<UpdateModel> updateModelValidator;
    private readonly IModelValidator<CreateModel> createModelValidator;

    public CategoryService(IDbContextFactory<MainDbContext> dbContextFactory,IMapper mapper, IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator, IAction action) 
    { 
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.action = action;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }
    public async Task<IEnumerable<CategoryModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var categories = await context.Categories.ToListAsync();

        var result = categories.Select(x => new CategoryModel()
        {
            Id = x.Uid,
            Title = x.Title,
        });
        return result;
    }
    public async Task<CategoryModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Uid == id);


        var result = new CategoryModel()
        {
            Id = category.Uid,
            Title = category.Title,
        };
        return result;
    }      
    public async Task<CategoryModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();
        
        var category = new Category()
        {
            Title = model.Title,
        };
            //mapper.Map<Product>(model);

        await context.Categories.AddAsync(category);

        await context.SaveChangesAsync();

        //await action.PublicateBook(new PublicateCategoryModel()
        //{
        //    Id = category.Id,
        //    Title = category.Title,
        //});
        var result = new CategoryModel()
        {
            Id = category.Uid,
            Title = category.Title,

        };
        return result;
    }
    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.Where(x => x.Uid == id).FirstOrDefaultAsync();

        category.Title = model.Title;


        context.Categories.Update(category);

        await context.SaveChangesAsync();

    }
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var product = await context.Products.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (product == null)
            throw new ProcessException($"Product (ID = {id}) not found.");

        context.Products.Remove(product);

        await context.SaveChangesAsync();
    }
}
