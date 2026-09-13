using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.DTOs.Sessions;
using Gym.Presentation.ViewModels.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gym.Presentation.Extensions;

namespace Gym.Presentation.Controllers;

public class SessionsController(
    ISessionService sessionService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sessions = await sessionService.GetAllAsync(cancellationToken);
        var viewModel = sessions.Select(session => new SessionListItemViewModel
        {
            Id = session.Id,
            CategoryName = session.CategoryName,
            Description = session.Description,
            TrainerName = session.TrainerName,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Capacity = session.Capacity,
            AvailableSlots = session.AvailableSlots
        }).ToList();

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Createp(CreateSessionViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)

            return View(model);

        var result = await sessionService.CreateAsync(new CreateSessionDto
        {
            Description = model.Description,
            Capacity = model.Capacity,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            TrainerId = model.TrainerId,
            CategoryId = model.CategoryId
        }, cancellationToken);

        if (result.IsFailure)
        {
            TempData["ErrorMessage"] = result.Error.Description;
            ModelState.AddModelError(nameof(result.Error.Code), result.Error.Description);
            return View(model);
        }
        TempData["SuccessMessage"] = "Session created successfully.";
        return RedirectToAction(nameof(Index));



    }


  

    
}
