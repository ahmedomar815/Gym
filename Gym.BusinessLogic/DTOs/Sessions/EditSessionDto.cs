namespace Gym.BusinessLogic.DTOs.Sessions;

public sealed class EditSessionDto
{
    public int Id { get; init; }

    public int? TrainerId { get; init; }

    public int CategoryId { get; init; }

    public string Description { get; init; } = string.Empty;

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }
}
