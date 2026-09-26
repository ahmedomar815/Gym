using Gym.DataAccess.Repositories;
using Gym.BusinessLogic.DTOs.Bookings;
using Gym.BusinessLogic.Results;
using Gym.DataAccess.Models;
using Gym.DataAceess.Specificaiton.Sessions;
using Gym.DataAceess.Specificaiton.Bookings;
using Mapster;

namespace Gym.BusinessLogic.Services;

internal sealed class BookingService(IUniteOfWork unitOfWork) : IBookingService
{
    public Task<bool> HasBookingsForMemberAsync(int memberId, CancellationToken cancellationToken = default)
    {
        return unitOfWork.Members.HasBookingsAsync(memberId, cancellationToken);
    }

    public async Task<IReadOnlyList<BookingListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var bookings = await unitOfWork.Bookings.GetAllWithSpecificationAsync(
            new BookingWithMemberSessionDetails(), cancellationToken);
        return bookings.Adapt<List<BookingListItemDto>>();
    }

    public async Task<BookingFormDto> GetCreateFormAsync(CancellationToken cancellationToken = default)
    {
        var members = await unitOfWork.Members.GetAllAsync(cancellationToken);
        var availableSessions = await unitOfWork.Sessions.GetAllWithDetailsAsync(cancellationToken);

        return new BookingFormDto
        {
            Members = members
                .Where(member => member.IsActive)
                .OrderBy(member => member.Name)
                .Adapt<List<BookingMemberOptionDto>>(),
            Sessions = availableSessions
                .Where(session => session.StartTime > DateTime.Now && session.Bookings.Count < session.Capacity)
                .OrderBy(session => session.StartTime)
                .Adapt<List<BookingSessionOptionDto>>()
        };
    }

    public async Task<Result> CreateAsync(CreateBookingDto model, CancellationToken cancellationToken = default)
    {
        var member = await unitOfWork.Members.GetByIdAsync(model.MemberId, cancellationToken);
        if (member is null || !member.IsActive)
            return Result.Failure("The selected member is unavailable.", nameof(model.MemberId));

        var session = await unitOfWork.Sessions.GetEntityWithSpecificationAsync(
            new SessionWithTrainerCategoryAndBookingById(model.SessionId), cancellationToken);
        if (session is null || session.StartTime <= DateTime.Now)
            return Result.Failure("The selected session is no longer available.", nameof(model.SessionId));

        if (session.Bookings.Count >= session.Capacity)
            return Result.Failure("This session is already full.", nameof(model.SessionId));

        if (session.Bookings.Any(booking => booking.MemberId == model.MemberId))
            return Result.Failure("This member already has a booking for the selected session.", nameof(model.MemberId));

        await unitOfWork.Bookings.AddAsync(new Booking
        {
            MemberId = model.MemberId,
            SessionId = model.SessionId
        }, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }
}
