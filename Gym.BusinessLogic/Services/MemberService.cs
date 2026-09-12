using Gym.BusinessLogic.DTOs.Members;
using Gym.BusinessLogic.Results;
using Gym.DataAccess.Data.Configuration;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Gym.DataAceess.Repositories;


namespace Gym.BusinessLogic.Services;

internal sealed class MemberService(IMemberRepository memberRepository ,IRepository<HealthyRecord> healthRepository) : IMemberService
{
    public async Task<IReadOnlyList<MemberListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var members = await memberRepository.GetAllAsync(cancellationToken);
        return members.Select(member => new MemberListItemDto
        {
            Id = member.Id,
            PhotoUrl = member.PhotoUrl,
            Name = member.Name,
            Email = member.Email,
            Gender = member.Gender.ToString(),
            PhoneNumber = member.PhoneNumber
        }).ToList();
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

    public async Task<EditMemberDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await memberRepository.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return null;
        }

        return new EditMemberDto
        {
           
            Name = member.Name,
            Email = member.Email,
            Phone = member.PhoneNumber,
            BuildingNumber = member.Address.BuidingNumber,
            City = member.Address.City,
            Street = member.Address.Street,
            PhotoUrl = member.PhotoUrl
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

    public async Task<Result> UpdateAsync(int id,EditMemberDto model, CancellationToken cancellationToken = default)
    {
        var member = await memberRepository.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return Result.Failure(new Error(
                nameof(id),
                "Member not found."));
        }

        var email = model.Email.Trim().ToLower();
        var phoneNumber = model.Phone.Trim();
        if(model.Name != member.Name)
        {
            return Result.Failure(new Error(
               nameof(model.Name),
               "This Name is Changed"));
        }
        if (await memberRepository.IsPhoneTakenAsync(phoneNumber, cancellationToken, id))
        {
            return Result.Failure(new Error(
                nameof(model.Phone),
                "A member with this phone number already exists."));
        }

        if (await memberRepository.IsEmailTakenAsync(email, cancellationToken, id))
        {
            return Result.Failure(new Error(
                nameof(model.Email),
                "A member with this email address already exists."));
        }

        member.Name = model.Name.Trim();
        member.Email = email;
        member.PhoneNumber = phoneNumber;
        member.PhotoUrl = model.PhotoUrl;
        member.Address.BuidingNumber = model.BuildingNumber;
        member.Address.City = model.City.Trim();
        member.Address.Street = model.Street.Trim();

        memberRepository.Update(member);
        await memberRepository.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await memberRepository.GetByIdAsync(id,   cancellationToken );
        var healthRecord = await healthRepository.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return null!;
        }

        memberRepository.Delete(member);
        healthRepository.Delete(healthRecord!);
        await memberRepository.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }


}
