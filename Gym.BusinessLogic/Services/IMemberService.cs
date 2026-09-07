using Gym.BusinessLogic.DTOs;

namespace Gym.BusinessLogic.Services;

public interface IMemberService
{
    Task<IReadOnlyList<MemberListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<int> CreateAsync(CreateMemberDto createMemberDto, CancellationToken cancellationToken = default);
}
