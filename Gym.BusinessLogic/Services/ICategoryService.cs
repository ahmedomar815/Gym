using Gym.BusinessLogic.DTOs.Categories;

namespace Gym.BusinessLogic.Services;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
