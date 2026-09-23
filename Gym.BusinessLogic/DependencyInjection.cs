using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Gym.BusinessLogic.AttachmentRules;

namespace Gym.BusinessLogic;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        MapsterConfig.Register();
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IHealthRecordService, HealthRecordService>();
        services.AddScoped<IAttachmentService, AttachmentService>();
        return services;
    }
}
