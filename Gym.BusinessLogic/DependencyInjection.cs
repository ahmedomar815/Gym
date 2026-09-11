using Gym.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.BusinessLogic;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IHealthRecordService, HealthRecordService>();
        return services;
    }
}
