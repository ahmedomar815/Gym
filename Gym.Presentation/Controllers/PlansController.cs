using Gym.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Gym.Presentation.ViewModels.Plans;
using Gym.BusinessLogic.DTOs.Plans;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

[Authorize]
public class PlansController(IPlanService planService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var plans = await planService.GetAllAsync(cancellationToken);
        return View(plans.Adapt<List<PlanViewModel>>());
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var plan = await planService.GetByIdAsync(id, cancellationToken);
        if (plan == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(plan.Adapt<PlanViewModel>());
    }
}
