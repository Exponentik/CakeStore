namespace CakeStore.Worker;

using CakeStore.Services.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using CakeStore.Services.Logger;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddAppLogger()
            .AddRabbitMq()            
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}