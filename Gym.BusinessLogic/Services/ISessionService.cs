using Gym.BusinessLogic.DTOs.Sessions;
using Gym.BusinessLogic.DTOs.Categories;
using Gym.BusinessLogic.DTOs.Trainers;
using Gym.BusinessLogic.Results;


namespace Gym.BusinessLogic.Services;

public interface ISessionService
{
    Task<IReadOnlyList<SessionListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateSessionDto model, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, EditSessionDto model, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<SessionDetailsDto>> GetDetailsByID(int Id, CancellationToken cancellationToken = default);
    Task<SessionDetailsDto?> GetForDeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<EditSessionDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CategoryDto>> GetCreateCategoriesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TrainerDto>> GetTrainersByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
}
