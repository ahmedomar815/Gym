using Gym.BusinessLogic.DTOs.Sessions;
using Gym.BusinessLogic.Results;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Mapster;

namespace Gym.BusinessLogic.Services;

internal sealed class SessionService(IUnitOfWork unitOfWork) : ISessionService
{
    public async Task<IReadOnlyList<SessionListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sessions = await unitOfWork.Sessions.GetAllWithDetailsAsync(cancellationToken);

        return sessions.Adapt<List<SessionListItemDto>>();
    }


  

    public async Task<Result> CreateAsync(CreateSessionDto model, CancellationToken cancellationToken = default)
    {
        if (model.EndTime <= model.StartTime)
        {
            return Result.Failure(new Error(nameof(model.EndTime), "The end time must be after the start time."));
        }

        if(!await unitOfWork.Trainers.ExistAsync(x=>x.Id==model.TrainerId, cancellationToken) )
        {
            return Result.Failure(new Error(nameof(model.TrainerId), "The specified trainer does not exist."));
        }
        if (!await unitOfWork.Categories.ExistAsync(x => x.Id == model.CategoryId, cancellationToken))
        {
            return Result.Failure(new Error(nameof(model.TrainerId), "The specified trainer does not exist."));
        }

        await unitOfWork.Sessions.AddAsync(new Session
        {
            Description = model.Description.Trim(),
            Capacity = model.Capacity,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            TrainerId = model.TrainerId,
            CategoryId = model.CategoryId
        }, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
