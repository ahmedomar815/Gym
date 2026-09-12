using Gym.BusinessLogic.DTOs.Members;
using Gym.BusinessLogic.Results;

namespace Gym.BusinessLogic.Services;

public interface IMemberService
{
    Task<IReadOnlyList<MemberListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<MemberDetailsDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<EditMemberDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateMemberDto createMemberDto, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(int id,EditMemberDto model, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
