using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.DTOs.Sessions;
using Gym.Presentation.ViewModels.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gym.Presentation.Extensions;
using Mapster;

namespace Gym.Presentation.Controllers;

public class SessionsController(
    ISessionService sessionService,
    ITrainerService trainerService,
    ICategoryService categoryService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sessions = await sessionService.GetAllAsync(cancellationToken);
        var viewModel = sessions.Adapt<List<SessionListItemViewModel>>();

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View(await CreateViewModelAsync(cancellationToken));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSessionViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        var result = await sessionService.CreateAsync(model.Adapt<CreateSessionDto>(), cancellationToken);

        if (result.IsFailure)
        {
            TempData["ErrorMessage"] = result.Error.Description;
            ModelState.AddModelError(nameof(result.Error.Code), result.Error.Description);
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }
        TempData["SuccessMessage"] = "Session created successfully.";
        return RedirectToAction(nameof(Index));



    }

    private async Task<CreateSessionViewModel> CreateViewModelAsync(CancellationToken cancellationToken)
    {
        var model = new CreateSessionViewModel();
        await PopulateLookupsAsync(model, cancellationToken);
        return model;
    }

    private async Task PopulateLookupsAsync(CreateSessionViewModel model, CancellationToken cancellationToken)
    {
        var trainersTask = trainerService.GetAllAsync(cancellationToken);
        var categoriesTask = categoryService.GetAllAsync(cancellationToken);
        await Task.WhenAll(trainersTask, categoriesTask);

        model.Trainers = trainersTask.Result.Adapt<List<SelectListItem>>();
        model.Categories = categoriesTask.Result.Adapt<List<SelectListItem>>();
    }
}
