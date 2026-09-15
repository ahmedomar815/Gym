using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;

namespace Gym.DataAccess.Repositories;

internal sealed class TrainerRepository(GymDbContext context) : Repository<Trainer>(context), ITrainerRepository
{
}
