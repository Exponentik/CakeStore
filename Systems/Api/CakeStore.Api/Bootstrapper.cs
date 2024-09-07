namespace CakeStore.Api;

using CakeStore.Services.Logger;
using CakeStore.Services.Settings;

using CakeStore.Api.Settings;
using CakeStore.Context.Seeder;
using CakeStore.Services.Products;
using CakeStore.Services.RabbitMq;
using CakeStore.Services.Actions;
using CakeStore.Services.UserAccount;
using CakeStore.Services.Categories;
using CakeStore.Services.Reviews;
using CakeStore.Services.Likes;
using CakeStore.Services.Images;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service, IConfiguration configuration = null)
    {
        service.AddMainSettings()
                .AddLogSettings()
                .AddSwaggerSettings()
                .AddIdentitySettings()
                .AddAppLogger()
                .AddDbSeeder()
                .AddApiSpecialSettings()
                .AddProductService()
                .AddCategoryService()
                .AddRabbitMq()
                .AddActions()
                .AddUserAccountService()
                .AddReviewService()
                .AddLikeService()
                .AddImageService()
                ;

        return service;
    }
}
