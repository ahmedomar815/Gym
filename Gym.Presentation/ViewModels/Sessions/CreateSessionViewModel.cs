using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.ViewModels.Sessions;

public class CreateSessionViewModel
{
    [Required]
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 25)]
    public int Capacity { get; set; }

    [Required]
    [Display(Name = "Start time")]
    public DateTime StartTime { get; set; }

    [Required]
    [Display(Name = "End time")]
    public DateTime EndTime { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a trainer.")]
    [Display(Name = "Trainer")]
    public int TrainerId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public IEnumerable<SelectListItem> Trainers { get; set; } = [];

    public IEnumerable<SelectListItem> Categories { get; set; } = [];
}
