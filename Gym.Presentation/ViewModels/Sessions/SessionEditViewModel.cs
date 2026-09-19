using System.ComponentModel.DataAnnotations;

namespace Gym.Presentation.ViewModels.Sessions;

public class SessionEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Trainer is required")]
    [Display(Name = "Trainer")]
    public int? TrainerId { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start date is required")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Start Date & Time")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required")]
    [DataType(DataType.DateTime)]
    [Display(Name = "End Date & Time")]
    public DateTime EndDate { get; set; }
}