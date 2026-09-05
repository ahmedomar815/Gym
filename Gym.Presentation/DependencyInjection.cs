using Gym.BusinessLogic;
using Gym.DataAccess;

namespace Presentation;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddBusinessLogicServices();
        services.AddDataAccessServices(configuration);
        return services;
    }
}
