using Microsoft.Extensions.DependencyInjection;

namespace Gym.BusinessLogic;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        // Register business services here as they are added to this layer.
        return services;
    }
}
