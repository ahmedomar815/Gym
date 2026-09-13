using Gym.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.BusinessLogic;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IHealthRecordService, HealthRecordService>();
        return services;
    }
}
