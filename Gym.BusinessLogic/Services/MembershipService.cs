using Gym.BusinessLogic.DTOs.Memberships;
using Gym.BusinessLogic.Results;
using Gym.DataAccess.Models;
using Gym.BusinessLogic.Repositories;
using Mapster;

namespace Gym.BusinessLogic.Services;

internal sealed class MembershipService(IUniteOfWork unitOfWork) : IMembershipService
{
    public async Task<IReadOnlyList<MembershipListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var memberships = await unitOfWork.Memberships.GetAllWithMemberAndPlanAsync(cancellationToken);

        return memberships.Adapt<List<MembershipListItemDto>>();
    }

    public async Task<MembershipCreateFormDto> GetCreateFormAsync(CancellationToken cancellationToken = default)
    {
        var members = await unitOfWork.Members.GetAllAsync(cancellationToken);
        var plans = await unitOfWork.Plans.GetAllAsync(cancellationToken);

        return new MembershipCreateFormDto
        {
            Members = members
                .OrderBy(member => member.Name)
                .Select(member => new MembershipMemberOptionDto
                {
                    Id = member.Id,
                    Name = member.Name
                })
                .ToList(),
            Plans = plans
                .Where(plan => plan.IsActive)
                .OrderBy(plan => plan.Name)
                .Select(plan => new MembershipPlanOptionDto
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    DurationInDays = plan.DurationInDays,
                    Price = plan.Price
                })
                .ToList()
        };
    }

    public async Task<Result> CreateAsync(CreateMembershipDto membership, CancellationToken cancellationToken = default)
    {
        var member = await unitOfWork.Members.GetByIdAsync(membership.MemberId, cancellationToken);
        if (member is null)
        {
            return Result.Failure("The selected member was not found.", nameof(membership.MemberId));
        }

        var plan = await unitOfWork.Plans.GetByIdAsync(membership.PlanId, cancellationToken);
        if (plan is null || !plan.IsActive)
        {
            return Result.Failure("The selected plan is unavailable.", nameof(membership.PlanId));
        }

        var startDate = membership.StartDate.Date;
        var hasActiveMembership = await unitOfWork.Memberships.ExistAsync(
            existing => existing.MemberId == membership.MemberId && existing.EndDate > startDate,
            cancellationToken);
        if (hasActiveMembership)
        {
            return Result.Failure("This member already has an active membership.", nameof(membership.MemberId));
        }

        await unitOfWork.Memberships.AddAsync(new Membership
        {
            MemberId = membership.MemberId,
            PlanId = membership.PlanId,
            StartDate = startDate,
            EndDate = startDate.AddDays(plan.DurationInDays)
        }, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
