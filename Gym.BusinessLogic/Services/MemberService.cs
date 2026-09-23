using Gym.BusinessLogic.AttachmentRules;
using Gym.BusinessLogic.DTOs.Members;
using Gym.BusinessLogic.Results;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Gym.DataAceess.Repositories;
using Gym.DataAceess.Specificaiton.Members;
using Mapster;


namespace Gym.BusinessLogic.Services;

internal sealed class MemberService(IUniteOfWork UniteOfWork, IBookingService bookingService,IAttachmentService attachmentService) : IMemberService
{
    private readonly IUniteOfWork _uniteOfWork = UniteOfWork;
    private readonly IAttachmentService _attachmentService = attachmentService;

    public async Task<IReadOnlyList<MemberListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var members = await _uniteOfWork.Members.GetAllAsync(cancellationToken);
        return members.Adapt<List<MemberListItemDto>>();
    }

    public async Task<MemberDetailsDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var specification = new MemberWithMembershipsAndPlanSpecification(id);
        var member = await _uniteOfWork.Members.GetEntityWithSpecificationAsync(specification, cancellationToken);
        return member?.Adapt<MemberDetailsDto>();
    }

    public async Task<EditMemberDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await _uniteOfWork.Members.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return null;
        }

        return member.Adapt<EditMemberDto>();
    }

    public async Task<Result> CreateAsync(CreateMemberDto model, CancellationToken cancellationToken = default)
    {
        

        var email = model.Email.Trim().ToLower();
        var phoneNumber = model.Phone.Trim();

        if (await _uniteOfWork.Members.IsEmailTakenAsync(email, cancellationToken))
        {
            return Result.Failure(
                "A member with this email address already exists.",
                nameof(model.Email));
        }

        if (await _uniteOfWork.Members.IsPhoneTakenAsync(phoneNumber, cancellationToken))
        {
            return Result.Failure(
                "A member with this phone number already exists.",
                nameof(model.Phone));
        }

        var member = model.Adapt<Member>();
        member.Name = model.Name.Trim();
        member.Email = email;
        member.PhoneNumber = phoneNumber;
        if (model.Photo is { Length:>0 })
        {
            var savePhoto= await _attachmentService.SaveAsync(model.Photo, AttachmentsCategories.Members, cancellationToken);
            if(savePhoto.IsFailure)
            {
                return Result.Failure(savePhoto.Error!, savePhoto.ErrorCode);
            }
            member.PhotoUrl = savePhoto.Value;
        }
        await _uniteOfWork.Members.AddAsync(member, cancellationToken);
        await _uniteOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(int id,EditMemberDto model, CancellationToken cancellationToken = default)
    {
        var member = await _uniteOfWork.Members.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return Result.Failure(
                "Member not found.",
                nameof(id));
        }

        var email = model.Email.Trim().ToLower();
        var phoneNumber = model.Phone.Trim();
        if(model.Name != member.Name)
        {
            return Result.Failure(
                "This name is changed.",
                nameof(model.Name));
        }
        if (await _uniteOfWork.Members.IsPhoneTakenAsync(phoneNumber, cancellationToken, id))
        {
            return Result.Failure(
                "A member with this phone number already exists.",
                nameof(model.Phone));
        }

        if (await _uniteOfWork.Members.IsEmailTakenAsync(email, cancellationToken, id))
        {
            return Result.Failure(
                "A member with this email address already exists.",
                nameof(model.Email));
        }

        member.Name = model.Name.Trim();
        member.Email = email;
        member.PhoneNumber = phoneNumber;
        member.PhotoUrl = model.PhotoUrl;
        member.Address.BuidingNumber = model.BuildingNumber;
        member.Address.City = model.City.Trim();
        member.Address.Street = model.Street.Trim();

        _uniteOfWork.Members.Update(member);
        await _uniteOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await _uniteOfWork.Members.GetByIdAsync(id,   cancellationToken );
      
        if (member is null)
        {
            return Result.Failure(
                "Member not found.",
                nameof(id));
        }

        if (await bookingService.HasBookingsForMemberAsync(id, cancellationToken))
        {
            return Result.Failure(
                "The member cannot be deleted because they have existing bookings.",
                nameof(id));
        }

        var healthRecord = await _uniteOfWork.HealthyRecords.FindAsync(
          record => record.MemberId == id);
        if(!string.IsNullOrEmpty(member.PhotoUrl))
        {
            await _attachmentService.DeleteAsync(member.PhotoUrl, cancellationToken);
        }
         _uniteOfWork.Members.Delete(member);
        _uniteOfWork.HealthyRecords.Delete(healthRecord!);


        await _uniteOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }


}
