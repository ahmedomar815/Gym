using Gym.BusinessLogic.DTOs;
using Gym.DataAccess.Models;
using Gym.DataAccess.Models.Enums;
using Gym.DataAccess.Repositories;

namespace Gym.BusinessLogic.Services;

internal sealed class MemberService(IRepository<Member> memberRepository) : IMemberService
{
    public async Task<IReadOnlyList<MemberListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var members = await memberRepository.GetAllAsync(cancellationToken);
        return members.Select(ToDto).ToList();
    }

    public async Task<int> CreateAsync(CreateMemberDto createMemberDto, CancellationToken cancellationToken = default)
    {
        

        /*var nameParts = createMemberDto.Name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var member = new Member
        {
            FirstName = nameParts.FirstOrDefault() ?? string.Empty,
            LastName = string.Join(' ', nameParts.Skip(1)),
            Email = createMemberDto.Email.Trim(),
            PhoneNumber = createMemberDto.PhoneNumber.Trim(),
            DateOfBirth = createMemberDto.DateOfBirth,
            Gender = createMemberDto.Gender,
            IsActive = true,
            JoinDate = DateTime.UtcNow,
            Address = new Address { City = createMemberDto.City.Trim(), Street = createMemberDto.Street.Trim() },
            HealthyRecord = new HealthyRecord
            {
                HeightInCentimeters = createMemberDto.HeightInCentimeters,
                WeightInKilograms = createMemberDto.WeightInKilograms,
                BloodType = ParseBloodType(createMemberDto.BloodType)
            }
        };

        await memberRepository.AddAsync(member, cancellationToken);
        await memberRepository.SaveChangesAsync(cancellationToken);
        return member.Id;*/
    }

    private static MemberListItemDto ToDto(Member member) => new()
    {
        Id = member.Id,
        PhotoUrl = member.PhotoUrl,
        FirstName = member.FirstName,
        Email = member.Email,
        Gender = member.Gender.ToString(),
        PhoneNumber = member.PhoneNumber
    };

    private static BloodType ParseBloodType(string bloodType) => bloodType.Trim().ToUpperInvariant() switch
    {
        "A+" => BloodType.APositive,
        "A-" => BloodType.ANegative,
        "B+" => BloodType.BPositive,
        "B-" => BloodType.BNegative,
        "AB+" => BloodType.ABPositive,
        "AB-" => BloodType.ABNegative,
        "O+" => BloodType.OPositive,
        "O-" => BloodType.ONegative,
        _ => throw new ArgumentException("A valid blood type is required.", nameof(bloodType))
    };
}
