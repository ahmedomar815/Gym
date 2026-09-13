using Gym.BusinessLogic.Services;
using Gym.Presentation.ViewModels;
using Gym.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Gym.Presentation.ViewModels.Members;
using Gym.BusinessLogic.DTOs.Members;
using Gym.Presentation.Extensions;

namespace Gym.Presentation.Controllers;

public class MembersController(
    IMemberService memberService,
    IHealthRecordService healthRecordService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var members = await memberService.GetAllAsync(cancellationToken);
        var viewModel = members.Select(member => new MemberListItemViewModel
        {
            Id = member.Id,
            PhotoUrl = member.PhotoUrl,
            FirstName = member.Name,
            Email = member.Email,
            Gender = member.Gender.ToString(),
            PhoneNumber = member.PhoneNumber
        }).ToList();

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

        var viewModel = new MemberDetailsViewModel
        {
            Id = member.Id,
            Name = member.Name,
            PhotoUrl = member.PhotoUrl,
            Email = member.Email,
            Phone = member.Phone,
            Gender = member.Gender,
            DateOfBirth = member.DateOfBirth,
            Address = member.Address,
            PlanName = member.PlanName,
            MembershipStartDate = member.MembershipStartDate,
            MembershipEndDate = member.MembershipEndDate
        };

        return View(viewModel);
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

        var viewModel = new EditMemberViewModel
        {
            Id = id,
            Name = member.Name,
            Email = member.Email,
            Phone = member.Phone,
            BuildingNumber = member.BuildingNumber,
            City = member.City,
            Street = member.Street,
            PhotoUrl = member.PhotoUrl
        };

        return View("EditMember", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, EditMemberViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View("EditMember", model);
        }

        var dto = new EditMemberDto
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            BuildingNumber = model.BuildingNumber,
            City = model.City,
            Street = model.Street,
            PhotoUrl = model.PhotoUrl
        };

        var result = await memberService.UpdateAsync(id, dto, cancellationToken);
        if (result.IsFailure)
        {
            result.AddToModelState(ModelState);
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

        var viewModel = new HealthRecordViewModel
        {
            Height = healthRecord.Height,
            Weight = healthRecord.Weight,
            BloodType = healthRecord.BloodType,
            Note = healthRecord.Note
        };

        return View("MemberHealthRecord", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = new CreateMemberDto
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            BuildingNumber = model.BuildingNumber,
            City = model.City,
            Street = model.Street,
            HeightInCentimeters = model.HealthRecordViewModel.Height,
            WeightInKilograms = model.HealthRecordViewModel.Weight,
            BloodType = model.HealthRecordViewModel.BloodType
        };

        var result = await memberService.CreateAsync(dto, cancellationToken);

        if (result.IsFailure)
        {
            TempData["ErrorMessage"] = result.Error.Description;
            result.AddToModelState(ModelState);
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
            TempData["ErrorMessage"] = result.Error.Description;
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
