using Gym.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Gym.Presentation.ViewModels.Plans;
using Gym.BusinessLogic.DTOs.Plans;

namespace Presentation.Controllers;

public class PlansController(IPlanService planService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var plans = await planService.GetAllAsync(cancellationToken);
        return View(plans.Select(ToViewModel).ToList());
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var plan = await planService.GetByIdAsync(id, cancellationToken);
        if (plan == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(ToViewModel(plan));
    }

    private static PlanViewModel ToViewModel(PlanDto plan) => new()
    {
        Id = plan.Id,
        Name = plan.Name,
        Description = plan.Description,
        DurationInDays = plan.DurationInDays,
        Price = plan.Price,
        IsActive = plan.IsActive
    };
}
