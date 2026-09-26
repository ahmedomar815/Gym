using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Gym.Presentation.ViewModels.Bookings;

public sealed class CreateBookingViewModel
{
    [Display(Name = "Member")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a member.")]
    public int MemberId { get; set; }

    [Display(Name = "Training session")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a session.")]
    public int SessionId { get; set; }

    public IReadOnlyList<SelectListItem> Members { get; set; } = [];
    public IReadOnlyList<SelectListItem> Sessions { get; set; } = [];
}
