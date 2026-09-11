using Gym.BusinessLogic.DTOs;
using Gym.BusinessLogic.Results;

namespace Gym.BusinessLogic.Services;

public interface IMemberService
{
    Task<IReadOnlyList<MemberListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<MemberDetailsDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateMemberDto createMemberDto, CancellationToken cancellationToken = default);
}
