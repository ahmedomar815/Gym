using Gym.BusinessLogic.DTOs.HealthRecords;
using Gym.BusinessLogic.DTOs.Members;
using Gym.BusinessLogic.DTOs.Plans;
using Gym.BusinessLogic.DTOs.Sessions;
using Gym.BusinessLogic.DTOs.Trainers;
using Gym.BusinessLogic.DTOs.Categories;
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
        TypeAdapterConfig<SessionListItemDto, SessionListItemViewModel>.NewConfig();
        TypeAdapterConfig<MemberDetailsDto, MemberDetailsViewModel>.NewConfig();
        TypeAdapterConfig<EditMemberDto, EditMemberViewModel>.NewConfig();
        TypeAdapterConfig<HealthRecordDto, HealthRecordViewModel>.NewConfig();

        TypeAdapterConfig<MemberListItemDto, MemberListItemViewModel>.NewConfig()
            .Map(destination => destination.FirstName, source => source.Name);

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
