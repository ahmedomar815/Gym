namespace Gym.Presentation.ViewModels.Sessions;

using System;



public sealed class SessionDetailsViewModel
{
    public int Id { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string TrainerName { get; init; } = string.Empty;

    public int Capacity { get; init; }

    public int AvailableSlots { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public TimeSpan Duration => EndDate - StartDate;

    public string Status { get; init; } = string.Empty;
}
