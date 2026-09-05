using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class PlansController(IRepository<Plan> planRepository) : Controller
{
    private readonly IRepository<Plan> _planRepository = planRepository;

    public async Task<IActionResult> Index()
    {
        var plans = await _planRepository.GetAllAsync();
        return View(plans);
    }

    public async Task<IActionResult> Details(int id)
    {
        var plan = await _planRepository.GetByIdAsync(id);
        if (plan == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(plan);
    }
}
