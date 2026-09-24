using Gym.BusinessLogic;
using Gym.DataAccess;
using Gym.DataAceess.Data.Identity;
using Gym.Presentation.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Presentation;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,IConfiguration configuration)
    {
        MapsterConfig.Register();
        services.AddControllersWithViews();
        services.AddBusinessLogicServices();
        services.AddDataAccessServices(configuration);
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        services.ConfigureApplicationCookie(options =>
        {
        
            options.ExpireTimeSpan = TimeSpan.FromHours(10);
            options.SlidingExpiration = true;
        }
        
        );
        services.AddIdentityCore<ApplicationUser>()
            .AddSignInManager();
        return services;
    }
}
