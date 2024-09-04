using CakeStore.Services.Categories;
using Microsoft.Extensions.DependencyInjection;

namespace CakeStore.Services.Categories;

public static class Bootstrapper
{
    public static IServiceCollection AddCategoryService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICategoryService,CategoryService>();
            
    }
}
