using Gym.BusinessLogic.DTOs.Trainers;
using Gym.BusinessLogic.Results;

namespace Gym.BusinessLogic.Services;

public interface ITrainerService
{
    Task<IReadOnlyList<TrainerListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TrainerFormDto> GetFormAsync(CancellationToken cancellationToken = default);
    Task<CreateTrainerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> CreateAsync(CreateTrainerDto trainer, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, CreateTrainerDto trainer, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
