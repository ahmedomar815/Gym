using Gym.BusinessLogic.DTOs;
using Gym.BusinessLogic.Services;
using Gym.Presentation.ViewModels;
using Gym.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Presentation.Controllers;

public class MembersController(IMemberService memberService) : Controller
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

        return View(ToMemberDetailsViewModel(member));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
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
            ModelState.AddModelError(result.Error.Code, result.Error.Description);
            return View(model);
        }

        TempData["SuccessMessage"] = "Member created successfully.";
        return RedirectToAction(nameof(Index));
    }

    private static MemberDetailsViewModel ToMemberDetailsViewModel(MemberDetailsDto member) => new()
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
}
