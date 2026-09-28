using Gym.BusinessLogic.DTOs.Trainers;
using Gym.BusinessLogic.Results;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.BusinessLogic.Services;

internal sealed class TrainerService(IUniteOfWork unitOfWork) : ITrainerService
{
    public async Task<IReadOnlyList<TrainerListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var trainers = await unitOfWork.Trainers.GetAllAsync(cancellationToken);
        var categories = await unitOfWork.Categories.GetAllAsync(cancellationToken);

        return trainers.OrderBy(trainer => trainer.Name).Select(trainer => new TrainerListItemDto
        {
            Id = trainer.Id, Name = trainer.Name, Email = trainer.Email, PhoneNumber = trainer.PhoneNumber,
            Speciality = trainer.Speciality,
            CategoryName = categories.FirstOrDefault(category => category.Id == trainer.CategoryId)?.Name ?? "—"
        }).ToList();
    }

    public async Task<TrainerFormDto> GetFormAsync(CancellationToken cancellationToken = default)
    {
        var categories = await unitOfWork.Categories.GetAllAsync(cancellationToken);
        return new TrainerFormDto
        {
            Categories = categories.OrderBy(category => category.Name)
                .Select(category => new TrainerCategoryOptionDto { Id = category.Id, Name = category.Name }).ToList()
        };
    }

    public async Task<CreateTrainerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var trainer = await unitOfWork.Trainers.GetByIdAsync(id, cancellationToken);
        return trainer is null ? null : ToDto(trainer);
    }

    public async Task<Result> CreateAsync(CreateTrainerDto trainer, CancellationToken cancellationToken = default)
    {
        var validation = await ValidateAsync(trainer, null, cancellationToken);
        if (validation.IsFailure) return validation;

        await unitOfWork.Trainers.AddAsync(ToEntity(trainer), cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(int id, CreateTrainerDto trainer, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.Trainers.GetByIdAsync(id, cancellationToken);
        if (entity is null) return Result.Failure("Trainer not found.", nameof(id));

        var validation = await ValidateAsync(trainer, id, cancellationToken);
        if (validation.IsFailure) return validation;

        entity.Name = trainer.Name.Trim(); entity.Email = trainer.Email.Trim().ToLowerInvariant();
        entity.PhoneNumber = trainer.PhoneNumber.Trim(); entity.DateOfBirth = trainer.DateOfBirth.Date;
        entity.Gender = trainer.Gender; entity.Speciality = trainer.Speciality; entity.CategoryId = trainer.CategoryId;
        entity.Address.BuidingNumber = trainer.BuildingNumber; entity.Address.Street = trainer.Street.Trim(); entity.Address.City = trainer.City.Trim();
        unitOfWork.Trainers.Update(entity);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var trainer = await unitOfWork.Trainers.GetByIdAsync(id, cancellationToken);
        if (trainer is null) return Result.Failure("Trainer not found.", nameof(id));
        if (await unitOfWork.Sessions.ExistAsync(session => session.TrainerId == id, cancellationToken))
            return Result.Failure("This trainer cannot be deleted because they have assigned sessions.");

        unitOfWork.Trainers.Delete(trainer);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    private async Task<Result> ValidateAsync(CreateTrainerDto trainer, int? excludedTrainerId, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.Categories.ExistAsync(category => category.Id == trainer.CategoryId, cancellationToken))
            return Result.Failure("The selected category does not exist.", nameof(trainer.CategoryId));

        var normalizedEmail = trainer.Email.Trim().ToLowerInvariant();
        if (await unitOfWork.Trainers.ExistAsync(item => item.Email == normalizedEmail && item.Id != excludedTrainerId, cancellationToken) ||
            await unitOfWork.Members.ExistAsync(item => item.Email == normalizedEmail, cancellationToken))
            return Result.Failure("This email address is already in use.", nameof(trainer.Email));

        var phone = trainer.PhoneNumber.Trim();
        if (await unitOfWork.Trainers.ExistAsync(item => item.PhoneNumber == phone && item.Id != excludedTrainerId, cancellationToken) ||
            await unitOfWork.Members.ExistAsync(item => item.PhoneNumber == phone, cancellationToken))
            return Result.Failure("This phone number is already in use.", nameof(trainer.PhoneNumber));

        return Result.Success();
    }

    private static Trainer ToEntity(CreateTrainerDto trainer) => new()
    {
        Name = trainer.Name.Trim(), Email = trainer.Email.Trim().ToLowerInvariant(), PhoneNumber = trainer.PhoneNumber.Trim(),
        DateOfBirth = trainer.DateOfBirth.Date, Gender = trainer.Gender, Speciality = trainer.Speciality,
        CategoryId = trainer.CategoryId, IsActive = true,
        Address = new Address { BuidingNumber = trainer.BuildingNumber, Street = trainer.Street.Trim(), City = trainer.City.Trim() }
    };

    private static CreateTrainerDto ToDto(Trainer trainer) => new()
    {
        Name = trainer.Name, Email = trainer.Email, PhoneNumber = trainer.PhoneNumber, DateOfBirth = trainer.DateOfBirth,
        Gender = trainer.Gender, BuildingNumber = trainer.Address.BuidingNumber, Street = trainer.Address.Street,
        City = trainer.Address.City, Speciality = trainer.Speciality, CategoryId = trainer.CategoryId
    };
}
