using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;

namespace Gym.DataAccess.Repositories;

internal sealed class CategoryRepository(GymDbContext context) : Repository<Category>(context), ICategoryRepository
{
}
