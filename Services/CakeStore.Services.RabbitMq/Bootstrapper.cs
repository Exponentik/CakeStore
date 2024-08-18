namespace CakeStore.Services.RabbitMq;

using CakeStore.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

public static class Bootstrapper
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<RabbitMqSettings>("RabbitMq", configuration);
        services.AddSingleton(settings);

        services.AddSingleton<IRabbitMq, RabbitMq>();

        return services;
    }
}
