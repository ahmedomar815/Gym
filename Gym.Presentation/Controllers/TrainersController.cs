using Gym.BusinessLogic.DTOs.Trainers;
using Gym.BusinessLogic.Services;
using Gym.Presentation.ViewModels.Trainers;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.Controllers;

[Authorize(Roles = "SuperAdmin")]
public class TrainersController(ITrainerService trainerService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View((await trainerService.GetAllAsync(cancellationToken)).Adapt<List<TrainerListItemViewModel>>());

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new TrainerFormViewModel();
        await PopulateCategoriesAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TrainerFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) { await PopulateCategoriesAsync(model, cancellationToken); return View(model); }
        var result = await trainerService.CreateAsync(model.Adapt<CreateTrainerDto>(), cancellationToken);
        if (result.IsFailure) { ModelState.AddModelError(result.ErrorCode ?? string.Empty, result.Error!); await PopulateCategoriesAsync(model, cancellationToken); return View(model); }
        TempData["SuccessMessage"] = "Trainer created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var trainer = await trainerService.GetByIdAsync(id, cancellationToken);
        if (trainer is null) return NotFound();
        var model = trainer.Adapt<TrainerFormViewModel>();
        await PopulateCategoriesAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TrainerFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) { await PopulateCategoriesAsync(model, cancellationToken); return View(model); }
        var result = await trainerService.UpdateAsync(id, model.Adapt<CreateTrainerDto>(), cancellationToken);
        if (result.IsFailure) { ModelState.AddModelError(result.ErrorCode ?? string.Empty, result.Error!); await PopulateCategoriesAsync(model, cancellationToken); return View(model); }
        TempData["SuccessMessage"] = "Trainer updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmation(int id, CancellationToken cancellationToken)
    {
        var result = await trainerService.DeleteAsync(id, cancellationToken);
        TempData[result.IsSuccess ? "SuccessMessage" : "ErrorMessage"] = result.IsSuccess ? "Trainer deleted successfully." : result.Error;
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCategoriesAsync(TrainerFormViewModel model, CancellationToken cancellationToken)
    {
        var form = await trainerService.GetFormAsync(cancellationToken);
        model.Categories = form.Categories.Select(category => new SelectListItem(category.Name, category.Id.ToString())).ToList();
    }
}
