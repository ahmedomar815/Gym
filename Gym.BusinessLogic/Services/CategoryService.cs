using Gym.BusinessLogic.DTOs.Categories;
using Gym.DataAccess.Repositories;
using Mapster;

namespace Gym.BusinessLogic.Services;

internal sealed class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await unitOfWork.Categories.GetAllAsync(cancellationToken);

        return categories.Adapt<List<CategoryDto>>();
    }
}
