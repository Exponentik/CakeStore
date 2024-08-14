namespace CakeStore.Api;

using CakeStore.Services.Logger;
using CakeStore.Services.Settings;
public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service, IConfiguration configuration = null)
    {
        service.AddMainSettings()
            .AddLogSettings()
                .AddSwaggerSettings()
                .AddAppLogger()
                ;

        return service;
    }
}
