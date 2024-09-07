using CakeStore.Services.Likes;
using Microsoft.Extensions.DependencyInjection;

namespace CakeStore.Services.Likes;

public static class Bootstrapper
{
    public static IServiceCollection AddLikeService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ILikeService, LikeService>();
            
    }
}
