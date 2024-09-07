using CakeStore.Services.Images;
using Microsoft.Extensions.DependencyInjection;

namespace CakeStore.Services.Images;

public static class Bootstrapper
{
    public static IServiceCollection AddImageService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IImageService, ImageService>();

    }
}
