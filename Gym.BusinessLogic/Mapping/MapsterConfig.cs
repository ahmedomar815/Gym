using Gym.BusinessLogic.DTOs.Categories;
using Gym.BusinessLogic.DTOs.Members;
using Gym.BusinessLogic.DTOs.Plans;
using Gym.BusinessLogic.DTOs.Sessions;
using Gym.BusinessLogic.DTOs.Trainers;
using Gym.DataAccess.Models;
using Mapster;

namespace Gym.BusinessLogic.Mapping;

public static class MapsterConfig
{
    public static void Register()
    {
        TypeAdapterConfig<Plan, PlanDto>.NewConfig();
        TypeAdapterConfig<Trainer, TrainerDto>.NewConfig();
        TypeAdapterConfig<Category, CategoryDto>.NewConfig();

        TypeAdapterConfig<Member, MemberListItemDto>.NewConfig()
            .Map(destination => destination.PhoneNumber, source => source.PhoneNumber)
            .Map(destination => destination.Gender, source => source.Gender.ToString());

        TypeAdapterConfig<Member, MemberDetailsDto>.NewConfig()
            .Map(destination => destination.Phone, source => source.PhoneNumber)
            .Map(destination => destination.Gender, source => source.Gender.ToString())
            .Map(destination => destination.Address,
                source => $"{source.Address.BuidingNumber} {source.Address.Street}, {source.Address.City}".Trim())
            .Map(destination => destination.PlanName,
                source => source.Memberships
                    .Where(membership => membership.IsActive)
                    .OrderByDescending(membership => membership.StartDate)
                    .Select(membership => membership.Plan.Name)
                    .FirstOrDefault() ?? string.Empty)
            .Map(destination => destination.MembershipStartDate,
                source => source.Memberships
                    .Where(membership => membership.IsActive)
                    .OrderByDescending(membership => membership.StartDate)
                    .Select(membership => (DateTime?)membership.StartDate)
                    .FirstOrDefault())
            .Map(destination => destination.MembershipEndDate,
                source => source.Memberships
                    .Where(membership => membership.IsActive)
                    .OrderByDescending(membership => membership.StartDate)
                    .Select(membership => (DateTime?)membership.EndDate)
                    .FirstOrDefault());

        TypeAdapterConfig<Member, EditMemberDto>.NewConfig()
            .Map(destination => destination.Phone, source => source.PhoneNumber)
            .Map(destination => destination.BuildingNumber, source => source.Address.BuidingNumber)
            .Map(destination => destination.City, source => source.Address.City)
            .Map(destination => destination.Street, source => source.Address.Street);

        TypeAdapterConfig<CreateMemberDto, Member>.NewConfig()
            .Map(destination => destination.PhoneNumber, source => source.Phone)
            .Map(destination => destination.IsActive, _ => true)
            .Map(destination => destination.JoinDate, _ => DateTime.UtcNow)
            .Map(destination => destination.Address, source => new Address
            {
                BuidingNumber = source.BuildingNumber,
                City = source.City.Trim(),
                Street = source.Street.Trim()
            })
            .Map(destination => destination.HealthyRecord, source => new HealthyRecord
            {
                HeightInCentimeters = source.HeightInCentimeters,
                WeightInKilograms = source.WeightInKilograms,
                BloodType = source.BloodType
            });

        TypeAdapterConfig<Session, SessionListItemDto>.NewConfig()
            .Map(destination => destination.CategoryName, source => source.Category.Name)
            .Map(destination => destination.TrainerName, source => source.Trainer.Name)
            .Map(destination => destination.AvailableSlots, source => source.Capacity - source.Bookings.Count);

        TypeAdapterConfig<Session, EditSessionDto>.NewConfig()
            .Map(destination => destination.StartDate, source => source.StartTime)
            .Map(destination => destination.EndDate, source => source.EndTime);
    }
}
