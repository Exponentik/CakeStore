namespace CakeStore.Services.UserAccount;

using AutoMapper;
using Azure.Core;
using CakeStore.Common.Exceptions;
using CakeStore.Common.Validator;
using CakeStore.Context;
using CakeStore.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserAccountService : IUserAccountService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;

    public UserAccountService(IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper, 
        UserManager<User> userManager ,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator
    )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<IEnumerable<UserAccountModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var reviews = await context.Users

            .ToListAsync();

        var result = reviews.Select(x => new UserAccountModel()
        {
            Id = x.Id,
            Name = x.FullName,
            Email = x.Email,

        });
        return result;
    }
    public async Task<UserAccountModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.Users
            //.Include(x => x.Reviews)

            //.Include(x => x.Images)

            .FirstOrDefaultAsync(x => x.Id == id);


        var result = new UserAccountModel()
        {
            Id = user.Id,
            Name = user.FullName,
            Email = user.Email
        };
        return result;
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        registerUserAccountModelValidator.Check(model);

        // Find user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        // Create user account
        user = new User()
        {
            Status = UserStatus.Active,
            FullName = model.Name,
            UserName = model.Email,  // Это логин. Мы будем его приравнивать к email, хотя это и не обязательно
            Email = model.Email,
            EmailConfirmed = true, // Так как это учебный проект, то сразу считаем, что почта подтверждена. В реальном проекте, скорее всего, надо будет ее подтвердить через ссылку в письме
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            Products = null,

            // ... Также здесь есть еще интересные свойства. Посмотрите в документации.
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        var res  = new UserAccountModel()
        {
            Id = user.Id,
            Name = user.FullName,
            Email = user.Email,
            
        };


        // Returning the created user
        return res;
    }
}
