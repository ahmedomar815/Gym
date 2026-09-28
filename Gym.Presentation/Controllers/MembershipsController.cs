using Gym.BusinessLogic.DTOs.Memberships;
using Gym.BusinessLogic.Services;
using Gym.Presentation.ViewModels.Memberships;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.Controllers;

[Authorize(Roles = "SuperAdmin")]
public class MembershipsController(
    IMembershipService membershipService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var memberships = await membershipService.GetAllAsync(cancellationToken);
        return View(memberships.Adapt<List<MembershipListItemViewModel>>());
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new CreateMembershipViewModel();
        await PopulateLookupsAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMembershipViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        var result = await membershipService.CreateAsync(model.Adapt<CreateMembershipDto>(), cancellationToken);
        if (result.IsFailure)
        {
            ModelState.AddModelError(result.ErrorCode ?? string.Empty, result.Error ?? "Unable to create membership.");
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        TempData["SuccessMessage"] = "Membership created successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateLookupsAsync(CreateMembershipViewModel model, CancellationToken cancellationToken)
    {
        var form = await membershipService.GetCreateFormAsync(cancellationToken);

        model.Members = form.Members
            .Select(member => new SelectListItem(member.Name, member.Id.ToString()))
            .ToList();
        model.Plans = form.Plans
            .Select(plan => new SelectListItem(
                $"{plan.Name} — {plan.DurationInDays} days — {plan.Price:N0} EGP",
                plan.Id.ToString()))
            .ToList();
    }
}
