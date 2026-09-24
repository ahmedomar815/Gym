using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.DTOs.Sessions;
using Gym.Presentation.ViewModels.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers;

[Authorize]
public class SessionsController(
    ISessionService sessionService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sessions = await sessionService.GetAllAsync(cancellationToken);
        var viewModel = sessions.Adapt<List<SessionListItemViewModel>>();

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var session = await sessionService.GetDetailsByID(id, cancellationToken);
        if (session.IsFailure)
        {
            TempData["ErrorMessage"] = session.Error;
            return RedirectToAction(nameof(Index));
        }

        var viewModel = session.Value.Adapt<SessionDetailsViewModel>();
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var session = await sessionService.GetForDeleteAsync(id, cancellationToken);
        if (session is null)
        {
            TempData["ErrorMessage"] = "Session not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(session.Adapt<DeleteSessionViewModel>());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteConfirmation(int id, CancellationToken cancellationToken)
    {
        var result = await sessionService.DeleteAsync(id, cancellationToken);
        if (result.IsFailure)
        {
            TempData["ErrorMessage"] = result.Error;
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Session deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new CreateSessionViewModel();
        await PopulateLookupsAsync(model, cancellationToken);
        return View(model);
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
            TempData["ErrorMessage"] = result.Error;
            ModelState.AddModelError(nameof(result.ErrorCode), result.Error!);
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }
        TempData["SuccessMessage"] = "Session created successfully.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var session = await sessionService.GetForEditAsync(id, cancellationToken);
        if (session is null)
        {
            TempData["ErrorMessage"] = "Session not found.";
            return RedirectToAction(nameof(Index));
        }

        await PopulateEditTrainersAsync(session.CategoryId, cancellationToken);
        return View(session.Adapt<SessionEditViewModel>());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(  int id,SessionEditViewModel model,CancellationToken cancellationToken)
    {
        if (id != model.Id)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            await PopulateEditTrainersForSessionAsync(id, cancellationToken);
            return View(model);
        }

        var result = await sessionService.UpdateAsync(
            id,
            model.Adapt<EditSessionDto>(),
            cancellationToken);

        if (result.IsFailure)
        {
            ModelState.AddModelError(result.ErrorCode ?? string.Empty, result.Error ?? "Unable to update the session.");
            await PopulateEditTrainersForSessionAsync(id, cancellationToken);
            return View(model);
        }

        TempData["SuccessMessage"] = "Session updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetTrainersByCategory(int categoryId, CancellationToken cancellationToken)
    {
        if (categoryId <= 0)
            return BadRequest();

        var trainers = await sessionService.GetTrainersByCategoryAsync(categoryId, cancellationToken);
        return Json(trainers);
    }

    private async Task PopulateLookupsAsync(CreateSessionViewModel model, CancellationToken cancellationToken)
    {
        var categories = await sessionService.GetCreateCategoriesAsync(cancellationToken);

        model.Categories = categories.Adapt<List<SelectListItem>>();
    }

    private async Task PopulateEditTrainersAsync(int categoryId, CancellationToken cancellationToken)
    {
        var trainers = await sessionService.GetTrainersByCategoryAsync(categoryId, cancellationToken);
        ViewBag.Trainers = trainers.Adapt<List<SelectListItem>>();
    }

    private async Task PopulateEditTrainersForSessionAsync(int sessionId, CancellationToken cancellationToken)
    {
        var session = await sessionService.GetForEditAsync(sessionId, cancellationToken);
        if (session is not null)
            await PopulateEditTrainersAsync(session.CategoryId, cancellationToken);
    }

}
