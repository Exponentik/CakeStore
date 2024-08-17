using AutoMapper;
using CakeStore.Common.Exceptions;
using CakeStore.Context;

using Microsoft.EntityFrameworkCore;
using System;


namespace CakeStore.Services.Products.Products;

public class ProductService : IProductService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public ProductService(IDbContextFactory<MainDbContext> dbContextFactory,IMapper mapper) 
    { 
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }
    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var products = await context.Products

            .Include(x => x.User).Include(x=>x.Categories).ToListAsync();

        var result = products.Select(x => new ProductModel()
        {
            Id = x.Uid,

            UserId = x.User.Uid,
            Name = x.Name,
            Description = x.Description,
            Categories = x.Categories.Select(s=>s.Title)
        });
        return result;
    }
    public async Task<ProductModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var product = await context.Products
            //.Include(x => x.Reviews)
            .Include(x => x.Categories)
            //.Include(x => x.Images)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<ProductModel>(product);
        return result;
    }      
    public async Task<ProductModel> Create(CreateModel model)
    {
        ////await createModelValidator.CheckAsync(model);

        //using var context = await dbContextFactory.CreateDbContextAsync();

        //var book = mapper.Map<Product>(model);

        //await context.Products.AddAsync(book);

        //await context.SaveChangesAsync();

        //await action.PublicateBook(new PublicateBookModel()
        //{
        //    Id = book.Id,
        //    Title = book.Title,
        //    Description = book.Description
        //});

        //return mapper.Map<ProductModel>(book);
        return null;
    }
    public async Task Update(Guid id, UpdateModel model)
    {
        ////await updateModelValidator.CheckAsync(model);

        //using var context = await dbContextFactory.CreateDbContextAsync();

        //var book = await context.Products.Where(x => x.Uid == id).FirstOrDefaultAsync();

        //book = mapper.Map(model, book);

        //context.Products.Update(book);

        //await context.SaveChangesAsync();

    }
    public async Task Delete(Guid id)
    {
        //using var context = await dbContextFactory.CreateDbContextAsync();

        //var book = await context.Products.Where(x => x.Uid == id).FirstOrDefaultAsync();

        //if (book == null)
        //    throw new ProcessException($"Book (ID = {id}) not found.");

        //context.Products.Remove(book);

        //await context.SaveChangesAsync();
    }

    Task<ProductModel> IProductService.Update(Guid id, UpdateModel model)
    {
        throw new NotImplementedException();
    }

    Task<ProductModel> IProductService.Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
