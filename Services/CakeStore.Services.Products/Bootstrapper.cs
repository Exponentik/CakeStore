using CakeStore.Services.Products;
using Microsoft.Extensions.DependencyInjection;

namespace CakeStore.Services.Products;

public static class Bootstrapper
{
    public static IServiceCollection AddProductService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IProductService,ProductService>();
            
    }
}
