namespace CakeStore.Api;

using CakeStore.Services.Logger;
using CakeStore.Services.Settings;

using CakeStore.Api.Settings;
using CakeStore.Context.Seeder;
using CakeStore.Services.Products;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service, IConfiguration configuration = null)
    {
        service.AddMainSettings()
            .AddLogSettings()
                .AddSwaggerSettings()
                .AddAppLogger()
                .AddDbSeeder()
                .AddApiSpecialSettings()
                .AddProductService()
                ;

        return service;
    }
}
