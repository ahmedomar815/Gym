using Gym.BusinessLogic;
using Gym.DataAccess;
using Gym.Presentation.Mapping;

namespace Presentation;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,IConfiguration configuration)
    {
        MapsterConfig.Register();
        services.AddControllersWithViews();
        services.AddBusinessLogicServices();
        services.AddDataAccessServices(configuration);
        return services;
    }
}
