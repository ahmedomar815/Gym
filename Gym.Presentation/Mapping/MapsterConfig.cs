using Gym.BusinessLogic.DTOs.HealthRecords;
using Gym.BusinessLogic.DTOs.Members;
using Gym.BusinessLogic.DTOs.Plans;
using Gym.BusinessLogic.DTOs.Sessions;
using Gym.BusinessLogic.DTOs.Trainers;
using Gym.BusinessLogic.DTOs.Categories;
using Gym.BusinessLogic.DTOs.Dashboard;
using Gym.Presentation.ViewModels.Dashboard;
using Gym.Presentation.ViewModels.Members;
using Gym.Presentation.ViewModels.Plans;
using Gym.Presentation.ViewModels.Sessions;
using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.Mapping;

public static class MapsterConfig
{
    public static void Register()
    {
        TypeAdapterConfig<PlanDto, PlanViewModel>.NewConfig();
        TypeAdapterConfig<DashboardDto, DashboardViewModel>.NewConfig();
        TypeAdapterConfig<DashboardDto, HomeViewModel>.NewConfig()
            .Map(destination => destination.Trainers, source => source.TotalTrainers);
        TypeAdapterConfig<SessionListItemDto, SessionListItemViewModel>.NewConfig();
        TypeAdapterConfig<SessionDetailsDto, SessionDetailsViewModel>.NewConfig()
            .Map(destination => destination.AvailableSlots,
                source =>  source.CountBooking)
            .Map(destination => destination.StartDate, source => source.StartTime)
            .Map(destination => destination.EndDate, source => source.EndTime)
            .Map(destination => destination.Status,
                source => source.StartTime > DateTime.Now
                    ? "Upcoming"
                    : source.EndTime >= DateTime.Now
                        ? "Ongoing"
                        : "Completed");
        TypeAdapterConfig<SessionDetailsDto, DeleteSessionViewModel>.NewConfig();
        TypeAdapterConfig<MemberDetailsDto, MemberDetailsViewModel>.NewConfig();
        TypeAdapterConfig<EditMemberDto, EditMemberViewModel>.NewConfig();
        TypeAdapterConfig<HealthRecordDto, HealthRecordViewModel>.NewConfig();

        TypeAdapterConfig<MemberListItemDto, MemberListItemViewModel>.NewConfig();

        TypeAdapterConfig<CreateMemberViewModel, CreateMemberDto>.NewConfig()
            .Map(destination => destination.HeightInCentimeters, source => source.HealthRecordViewModel.Height)
            .Map(destination => destination.WeightInKilograms, source => source.HealthRecordViewModel.Weight)
            .Map(destination => destination.BloodType, source => source.HealthRecordViewModel.BloodType);

        TypeAdapterConfig<TrainerDto, SelectListItem>.NewConfig()
            .Map(destination => destination.Value, source => source.Id.ToString())
            .Map(destination => destination.Text, source => source.Name);

        TypeAdapterConfig<CategoryDto, SelectListItem>.NewConfig()
            .Map(destination => destination.Value, source => source.Id.ToString())
            .Map(destination => destination.Text, source => source.Name);
    }
}
