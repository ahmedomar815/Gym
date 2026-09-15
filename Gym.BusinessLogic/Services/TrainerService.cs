using Gym.BusinessLogic.DTOs.Trainers;
using Gym.DataAccess.Repositories;
using Mapster;

namespace Gym.BusinessLogic.Services;

internal sealed class TrainerService(IUnitOfWork unitOfWork) : ITrainerService
{
    public async Task<IReadOnlyList<TrainerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var trainers = await unitOfWork.Trainers.GetAllAsync(cancellationToken);

        return trainers.Adapt<List<TrainerDto>>();
    }
}
