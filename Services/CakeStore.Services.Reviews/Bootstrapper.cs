using Microsoft.Extensions.DependencyInjection;

namespace CakeStore.Services.Reviews;

public static class Bootstrapper
{
    public static IServiceCollection AddReviewService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IReviewService,ReviewService>();
            
    }
}
