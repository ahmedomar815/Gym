using Gym.BusinessLogic.DTOs;
using Gym.BusinessLogic.Results;
using Gym.DataAccess.Models;
using Gym.DataAccess.Models.Enums;
using Gym.DataAccess.Repositories;
using Gym.DataAceess.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gym.BusinessLogic.Services;

internal sealed class MemberService(IMemberRepository memberRepository) : IMemberService
{
    public async Task<IReadOnlyList<MemberListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var members = await memberRepository.GetAllAsync(cancellationToken);
        return members.Select(ToDto).ToList();
    }

    public async Task<MemberDetailsDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await memberRepository.GetByIdWithMembershipsAndPlanAsync(id, cancellationToken);
        if (member is null)
        {
            return null;
        }

        var membership = member.Memberships
            .Where(item => item.IsActive)
            .OrderByDescending(item => item.StartDate)
            .FirstOrDefault();

        return new MemberDetailsDto
        {
            Id = member.Id,
            Name = member.Name,
            PhotoUrl = member.PhotoUrl,
            Email = member.Email,
            Phone = member.PhoneNumber,
            Gender = member.Gender.ToString(),
            DateOfBirth = member.DateOfBirth,
            Address = $"{member.Address.BuidingNumber} {member.Address.Street}, {member.Address.City}".Trim(),
            PlanName = membership?.Plan.Name ?? string.Empty,
            MembershipStartDate = membership is null
                ? null
                : DateOnly.FromDateTime(membership.StartDate),
            MembershipEndDate = membership is null
                ? null
                : DateOnly.FromDateTime(membership.EndDate)
        };
    }

    public async Task<Result> CreateAsync(CreateMemberDto model, CancellationToken cancellationToken = default)
    {
        

        var email = model.Email.Trim().ToLower();
        var phoneNumber = model.Phone.Trim();

        if (await memberRepository.IsEmailTakenAsync(model.Email,cancellationToken))
        {
            return Result.Failure(new Error(
               nameof(model.Email),
                "A member with this email address already exists."));
        }

        if (await memberRepository.IsPhoneTakenAsync(model.Phone, cancellationToken))
        {
            return Result.Failure(new Error(
                 nameof(model.Phone),
                "A member with this phone number already exists."));
        }

        var member = new Member
        {
            Name = model.Name.Trim(),
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            IsActive = true,
            JoinDate = DateTime.UtcNow,
            Address = new Address
            {
                BuidingNumber = model.BuildingNumber,
                City = model.City.Trim(),
                Street = model.Street.Trim()
            },
            HealthyRecord = new HealthyRecord
            {
                HeightInCentimeters = model.HeightInCentimeters,
                WeightInKilograms = model.WeightInKilograms,
                BloodType = model.BloodType
            }
        };

        await memberRepository.AddAsync(member, cancellationToken);
        await memberRepository.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    private static MemberListItemDto ToDto(Member member) => new()
    {
        Id = member.Id,
        PhotoUrl = member.PhotoUrl,
        Name = member.Name,
        Email = member.Email,
        Gender = member.Gender.ToString(),
        PhoneNumber = member.PhoneNumber
    };

}
