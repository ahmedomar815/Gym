using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAceess.Specificaiton.Members;

public sealed class MemberWithMembershipsAndPlanSpecification : Specification<Member>
{
    public MemberWithMembershipsAndPlanSpecification(int memberId)
    {
        Criteria = member => member.Id == memberId;
        AddInclude(query => query
            .Include(member => member.Memberships)
            .ThenInclude(membership => membership.Plan));
    }
}
