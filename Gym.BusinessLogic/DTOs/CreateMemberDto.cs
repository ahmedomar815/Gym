using Gym.DataAccess.Models.Enums;

namespace Gym.BusinessLogic.DTOs;

public sealed class CreateMemberDto
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public DateOnly DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public int BuildingNumber { get; init; }
    public string City { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public decimal HeightInCentimeters { get; init; }
    public decimal WeightInKilograms { get; init; }
    public BloodType BloodType { get; init; }
}
