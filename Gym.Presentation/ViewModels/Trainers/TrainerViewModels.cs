using Gym.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Gym.Presentation.ViewModels.Trainers;

public sealed class TrainerListItemViewModel
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public Speciality Speciality { get; init; }
    public string CategoryName { get; init; } = string.Empty;
}

public sealed class TrainerFormViewModel
{
    [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Enter a valid Egyptian mobile number.")]
    [Display(Name = "Phone number")] public string PhoneNumber { get; set; } = string.Empty;
    [Required, DataType(DataType.Date), Display(Name = "Date of birth")] public DateTime DateOfBirth { get; set; }
    [Display(Name = "Gender")] public Gender Gender { get; set; }
    [Range(1, 9000), Display(Name = "Building number")] public int BuildingNumber { get; set; }
    [Required, StringLength(150)] public string Street { get; set; } = string.Empty;
    [Required, StringLength(100)] public string City { get; set; } = string.Empty;
    [Display(Name = "Speciality")] public Speciality Speciality { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")] [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public IReadOnlyList<SelectListItem> Categories { get; set; } = [];
}
