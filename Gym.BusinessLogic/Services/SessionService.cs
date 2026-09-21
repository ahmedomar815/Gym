using Gym.BusinessLogic.DTOs.Sessions;
using Gym.BusinessLogic.DTOs.Categories;
using Gym.BusinessLogic.DTOs.Trainers;

using Gym.BusinessLogic.Results;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Gym.DataAceess.Specificaiton.Sessions;
using Mapster;

namespace Gym.BusinessLogic.Services;

internal sealed class SessionService(IUniteOfWork unitOfWork) : ISessionService
{
    public async Task<IReadOnlyList<CategoryDto>> GetCreateCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = await unitOfWork.Categories.GetAllAsync(cancellationToken);

        return categories.Adapt<List<CategoryDto>>();
    }

    public async Task<IReadOnlyList<TrainerDto>> GetTrainersByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        var trainers = await unitOfWork.Trainers.GetAllAsync(cancellationToken);

        return trainers
            .Where(trainer => trainer.CategoryId == categoryId)
            .OrderBy(x=> x.Name)
            .Adapt<List<TrainerDto>>();
    }

    public async Task<IReadOnlyList<SessionListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sessions = await unitOfWork.Sessions.GetAllWithDetailsAsync(cancellationToken);
        return sessions.Adapt<List<SessionListItemDto>>();
    }
    public async Task<Result<SessionDetailsDto>> GetDetailsByID(int Id, CancellationToken cancellationToken = default)
    {
       var session = await unitOfWork.Sessions.GetEntityWithSpecificationAsync(
           new SessionWithTrainerCategoryAndBookingById(Id),
           cancellationToken);
        if (session == null)
            return  Result.Failure<SessionDetailsDto>("Session not found.", nameof(Id));

       return Result.Success(session.Adapt<SessionDetailsDto>());

    }

    public async Task<EditSessionDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var session = await unitOfWork.Sessions.GetByIdAsync(id, cancellationToken);

        return session?.Adapt<EditSessionDto>();
    }

    public async Task<SessionDetailsDto?> GetForDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var session = await unitOfWork.Sessions.GetEntityWithSpecificationAsync(
            new SessionWithTrainerCategoryAndBookingById(id),
            cancellationToken);

        if (session is null)
            return null;

        return session.Adapt<SessionDetailsDto>();
    }

    public async Task<Result> CreateAsync(CreateSessionDto model, CancellationToken cancellationToken = default)
    {
        
        if (!await unitOfWork.Trainers.ExistAsync(x => x.Id == model.TrainerId, cancellationToken))
            return Result.Failure("The specified trainer does not exist.", nameof(model.TrainerId));
        if (!await unitOfWork.Categories.ExistAsync(x => x.Id == model.CategoryId, cancellationToken))
            return Result.Failure("The specified category does not exist.", nameof(model.CategoryId));
        if (!await IsTrainerAvailable(model.TrainerId, model.StartTime, model.EndTime, null, cancellationToken))
            return Result.Failure(
                "The specified trainer is not available during the specified time.",
                nameof(model.TrainerId));

        await unitOfWork.Sessions.AddAsync(new Session
        {
            Description = model.Description.Trim(), Capacity = model.Capacity,
            StartTime = model.StartTime, EndTime = model.EndTime,
            TrainerId = model.TrainerId, CategoryId = model.CategoryId
        }, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(int id, EditSessionDto model, CancellationToken cancellationToken = default)
    {
        var session = await unitOfWork.Sessions.GetByIdAsync(id, cancellationToken);
        if (session is null)
            return Result.Failure("Session not found.", nameof(id));

        if (session.StartTime <= DateTime.Now) return Result.Failure( "Completed or ongoing sessions cannot be updated.", nameof(id));

        if (!model.TrainerId.HasValue || model.TrainerId.Value <= 0)
            return Result.Failure("The specified trainer does not exist.", nameof(model.TrainerId));

        if (!await unitOfWork.Trainers.ExistAsync(trainer => trainer.Id == model.TrainerId.Value, cancellationToken))
            return Result.Failure("The specified trainer does not exist.", nameof(model.TrainerId));

        if (!await IsTrainerAvailable( model.TrainerId.Value,  model.StartDate,   model.EndDate, id,   cancellationToken))
            return Result.Failure(
                "The specified trainer is not available during the specified time.",
                nameof(model.TrainerId));

        session.TrainerId = model.TrainerId.Value;
        session.Description = model.Description.Trim();
        session.StartTime = model.StartDate;
        session.EndTime = model.EndDate;

        unitOfWork.Sessions.Update(session);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var session = await unitOfWork.Sessions.GetByIdAsync(id, cancellationToken);
        if (session is null)
            return Result.Failure("Session not found.", nameof(id));

        if (session.StartTime <= DateTime.Now)
            return Result.Failure("Only upcoming sessions can be deleted.", nameof(id));

        unitOfWork.Sessions.Delete(session);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    private async Task<bool> IsTrainerAvailable(   int trainerId,  DateTime startTime,    DateTime endTime,  int? excludedSessionId,
        CancellationToken cancellationToken)
    {
        var sessions = await unitOfWork.Sessions.GetAllAsync(cancellationToken);

        return !sessions.Any(session =>
            session.Id != excludedSessionId &&
            session.TrainerId == trainerId &&
            session.StartTime < endTime &&
            startTime < session.EndTime);
    }
}
