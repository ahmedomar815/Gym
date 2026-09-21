namespace Gym.Presentation.ViewModels.Dashboard;

public sealed class DashboardViewModel
{
    public int TotalMembers { get; init; }

    public int ActiveMembers { get; init; }

    public int TotalTrainers { get; init; }

    public int UpcomingSessions { get; init; }

    public int OngoingSessions { get; init; }

    public int CompletedSessions { get; init; }
}
