using Gym.BusinessLogic.Repositories;
using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;

namespace Gym.DataAccess.Repositories;

internal class PlanRepository(GymDbContext context):Repository<Plan>(context), IPlanRepository
{
}
