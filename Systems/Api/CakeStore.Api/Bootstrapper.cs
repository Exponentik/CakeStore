namespace CakeStore.Api;
using CakeStore.Services.Settings;
public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection services)
    {
        services.AddMainSettings()
                .AddSwaggerSettings()
                .AddLogSettings();

        return services;
    }
}
