using Gym.BusinessLogic.DTOs;
using Gym.BusinessLogic.Services;
using Gym.Presentation.ViewModels;
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
            FirstName = member.FirstName,
            Email = member.Email,
            Gender = member.Gender.ToString(),
            PhoneNumber = member.PhoneNumber
        }).ToList();

        return View(viewModel);
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

        await memberService.CreateAsync(new CreateMemberDto
        {
            Name = model.Name,
            Email = model.Email,
            PhoneNumber = model.Phone,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            City = model.City,
            Street = model.Street,
            HeightInCentimeters = model.HealthRecordViewModel.Height,
            WeightInKilograms = model.HealthRecordViewModel.Weight,
            BloodType = model.HealthRecordViewModel.BloodType
        }, cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}
