using Gym.BusinessLogic.DTOs.Dashboard;
using Gym.DataAccess.Repositories;

namespace Gym.BusinessLogic.Services;

internal sealed class DashboardService(IUniteOfWork unitOfWork) : IDashboardService
{
    public async Task<DashboardDto> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;

        return new DashboardDto
        {
            TotalMembers = await unitOfWork.Members.CountAsync(cancellationToken: cancellationToken),
            ActiveMembers = await unitOfWork.Members.CountAsync(
                member => member.Memberships.Any(membership => membership.EndDate > now),
                cancellationToken),

            TotalTrainers = await unitOfWork.Trainers.CountAsync(cancellationToken: cancellationToken),
            UpcomingSessions = await unitOfWork.Sessions.CountAsync(
                session => session.StartTime > now,
                cancellationToken),
            OngoingSessions = await unitOfWork.Sessions.CountAsync(
                session => session.StartTime <= now && session.EndTime >= now,
                cancellationToken),
            CompletedSessions = await unitOfWork.Sessions.CountAsync(
                session => session.EndTime < now,
                cancellationToken)
        };
    }
}
