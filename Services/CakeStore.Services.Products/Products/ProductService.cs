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


namespace CakeStore.Services.Products;

public class ProductService : IProductService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;
    private readonly IModelValidator<UpdateModel> updateModelValidator;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ProductService(IDbContextFactory<MainDbContext> dbContextFactory,IMapper mapper, IModelValidator<CreateModel> createModelValidator,
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
    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var products = await context.Products

            .Include(x => x.User).Include(x=>x.Categories).Include(x => x.Images).ToListAsync();

        var result = products.Select(x => new ProductModel()
        {
            Id = x.Uid,
            UserId = x.User.Id,
            Name = x.Name,
            Description = x.Description,
            Categories = x.Categories?.Select(s=>s.Title),
            Images = x.Images.Select(s => s),
        });
        return result;
    }
    public async Task<ProductModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var product = await context.Products
            //.Include(x => x.Reviews)
            .Include(x => x.Categories)
            .Include(x => x.Images)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Uid == id);


        var result = new ProductModel()
        {
            Id = product.Uid,
            UserId = product.User.Id,
            Name = product.Name,
            Description = product.Description,
            Categories = product.Categories.Select(s => s.Title),
            Images = product.Images.Select(s => s),
        };
        return result;
    }
    public async Task<ProductModel> Create(CreateModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        // Получение категорий из базы данных
        ICollection<Category> categories = new List<Category>();
        ICollection<Image> images = new List<Image>();
        foreach (var categoryTitle in model.Categories)
        {
            var category = await context.Categories
                .Where(x => x.Title == categoryTitle)
                .FirstOrDefaultAsync();

            if (category != null)
            {
                categories.Add(category);
            }
            else
            {
                throw new Exception($"Категория '{categoryTitle}' не найдена.");
            }
        }

        // Получение пользователя
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
        if (user == null)
        {
            throw new Exception("Пользователь не найден.");
        }

        // Создание нового продукта
        var product = new Product()
        {
            Name = model.Name,
            Description = model.Description,
            Categories = categories,
            User = user,
            Images = new List<Image>()  // Инициализируем список изображений
        };

        foreach (var img in model.Images)
        {
            byte[] fimg;
            using (var memoryStream = new MemoryStream())
            {
                await img.CopyToAsync(memoryStream);
                fimg =  memoryStream.ToArray();
            }
            var image = new Image()
            {
                ImageData = fimg,
                Product = product

            };

            product.Images.Add(image);
        }

        // Обработка и добавление изображений


        // Добавление продукта в контекст и сохранение изменений
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // Формирование результата для возврата
        var result = new ProductModel()
        {
            Id = product.Uid,
            UserId = product.User.Id,
            Name = product.Name,
            Description = product.Description,
            Categories = product.Categories.Select(s => s.Title),
            Images = product.Images
        };

        return result;
    }
    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var product = await context.Products.Where(x => x.Uid == id).FirstOrDefaultAsync();

        product.Name = model.Name;
        product.Description = model.Description;


        context.Products.Update(product);

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
