using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Gym.Presentation.ViewModels.Memberships;

public sealed class CreateMembershipViewModel
{
    [Display(Name = "Member")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a member.")]
    public int MemberId { get; set; }

    [Display(Name = "Membership plan")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a membership plan.")]
    public int PlanId { get; set; }

    [Display(Name = "Start date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;

    public IReadOnlyList<SelectListItem> Members { get; set; } = [];
    public IReadOnlyList<SelectListItem> Plans { get; set; } = [];
}
