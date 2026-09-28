using Gym.BusinessLogic.Results;
using Gym.BusinessLogic.DTOs.Memberships;

namespace Gym.BusinessLogic.Services;

public interface IMembershipService
{
    Task<IReadOnlyList<MembershipListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<MembershipCreateFormDto> GetCreateFormAsync(CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateMembershipDto membership, CancellationToken cancellationToken = default);
}
