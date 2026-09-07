using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.DataAceess.Repositories;

internal class PlanRepository(GymDbContext context):Repository<Plan>(context), IPlanRepository
{
}
