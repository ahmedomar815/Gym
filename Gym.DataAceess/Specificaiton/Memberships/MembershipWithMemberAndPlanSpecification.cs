using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAceess.Specificaiton.Memberships;

public sealed class MembershipWithMemberAndPlanSpecification : Specification<Membership>
{
    public MembershipWithMemberAndPlanSpecification()
    {
        AddInclude(query => query
            .Include(membership => membership.Member)
            .Include(membership => membership.Plan));

        AddOrderBy(query => query
            .OrderByDescending(membership => membership.EndDate));
    }
}
