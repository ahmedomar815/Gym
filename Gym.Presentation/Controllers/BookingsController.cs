using Gym.BusinessLogic.DTOs.Bookings;
using Gym.BusinessLogic.Services;
using Gym.Presentation.ViewModels.Bookings;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.Controllers;

[Authorize]
public class BookingsController(IBookingService bookingService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var bookings = await bookingService.GetAllAsync(cancellationToken);
        return View(bookings.Adapt<List<BookingListItemViewModel>>());
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new CreateBookingViewModel();
        await PopulateLookupsAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookingViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        var result = await bookingService.CreateAsync(model.Adapt<CreateBookingDto>(), cancellationToken);
        if (result.IsFailure)
        {
            ModelState.AddModelError(result.ErrorCode ?? string.Empty, result.Error ?? "Unable to create booking.");
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        TempData["SuccessMessage"] = "Booking created successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateLookupsAsync(CreateBookingViewModel model, CancellationToken cancellationToken)
    {
        var form = await bookingService.GetCreateFormAsync(cancellationToken);
        model.Members = form.Members
            .Select(member => new SelectListItem(member.Name, member.Id.ToString()))
            .ToList();
        model.Sessions = form.Sessions
            .Select(session => new SelectListItem(
                $"{session.CategoryName} — {session.StartTime:ddd, dd MMM h:mm tt} ({session.Capacity - session.BookedCount} spots left)",
                session.Id.ToString()))
            .ToList();
    }
}
