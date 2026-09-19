using Gym.BusinessLogic.Services;
using Gym.Presentation.ViewModels;
using Gym.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Gym.Presentation.ViewModels.Members;
using Gym.BusinessLogic.DTOs.Members;

using Mapster;

namespace Gym.Presentation.Controllers;

public class MembersController(
    IMemberService memberService,
    IHealthRecordService healthRecordService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var members = await memberService.GetAllAsync(cancellationToken);
        var viewModel = members.Adapt<List<MemberListItemViewModel>>();

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> MemberDetails(int id, CancellationToken cancellationToken)
    {
        var member = await memberService.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return NotFound();
        }

        return View(member.Adapt<MemberDetailsViewModel>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var member = await memberService.GetForEditAsync(id, cancellationToken);
        if (member is null)
        {
            return NotFound();
        }

        return View("EditMember", member.Adapt<EditMemberViewModel>());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, EditMemberViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View("EditMember", model);
        }

        var dto = model.Adapt<EditMemberDto>();

        var result = await memberService.UpdateAsync(id, dto, cancellationToken);
        if (result.IsFailure)
        {
           ModelState.AddModelError(result.ErrorCode ?? string.Empty, result.Error!);
            return View("EditMember", model);
        }

        TempData["SuccessMessage"] = "Member updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken cancellationToken)
    {
        var healthRecord = await healthRecordService.GetByMemberIdAsync(id, cancellationToken);
        if (healthRecord is null)
        {
            return NotFound();
        }

        return View("MemberHealthRecord", healthRecord.Adapt<HealthRecordViewModel>());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = model.Adapt<CreateMemberDto>();

        var result = await memberService.CreateAsync(dto, cancellationToken);

        if (result.IsFailure)
        {
            TempData["ErrorMessage"] = result.Error;
            ModelState.AddModelError(result.ErrorCode ?? string.Empty, result.Error!);
            return View(model);
        }

        TempData["SuccessMessage"] = "Member created successfully.";
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmation(int id, CancellationToken cancellationToken)
    {
        var result = await memberService.DeleteAsync(id, cancellationToken);
        if (result.IsFailure)
        {
            TempData["ErrorMessage"] = result.Error;
            return RedirectToAction(nameof(Index));
        }
        TempData["SuccessMessage"] = "Member deleted successfully.";
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var member = await memberService.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return NotFound();
        }
        ViewBag.id = id;
        return View("DeleteMember");
    }


}
