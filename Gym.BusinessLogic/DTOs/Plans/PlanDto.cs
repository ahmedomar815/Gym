namespace Gym.BusinessLogic.DTOs.Plans;

public sealed class PlanDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int DurationInDays { get; init; }

    public decimal Price { get; init; }

    public bool IsActive { get; init; }
}
