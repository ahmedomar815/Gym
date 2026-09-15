using Gym.BusinessLogic.DTOs.Trainers;

namespace Gym.BusinessLogic.Services;

public interface ITrainerService
{
    Task<IReadOnlyList<TrainerDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
