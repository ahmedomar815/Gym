using Gym.DataAccess.Models.Enums;

namespace Gym.BusinessLogic.DTOs.Trainers;

public sealed class TrainerListItemDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public Speciality Speciality { get; init; }
    public string CategoryName { get; init; } = string.Empty;
}

public sealed class CreateTrainerDto
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public int BuildingNumber { get; init; }
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public Speciality Speciality { get; init; }
    public int CategoryId { get; init; }
}

public sealed class TrainerFormDto
{
    public IReadOnlyList<TrainerCategoryOptionDto> Categories { get; init; } = [];
}

public sealed class TrainerCategoryOptionDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
