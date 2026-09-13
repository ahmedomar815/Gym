using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Repositories;
using Gym.DataAceess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.DataAccess;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDataAccessServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' was not found.");

        services.AddDbContext<GymDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
