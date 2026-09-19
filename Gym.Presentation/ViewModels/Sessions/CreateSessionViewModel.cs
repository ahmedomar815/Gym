using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.ViewModels.Sessions;

public class CreateSessionViewModel : IValidatableObject
{
    [Required]
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 25)]
    public int Capacity { get; set; }

    [Required]
    [Display(Name = "Start time")]
    public DateTime? StartTime { get; set; }

    [Required]
    [Display(Name = "End time")]
    public DateTime? EndTime { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a trainer.")]
    [Display(Name = "Trainer")]
    public int TrainerId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartTime.HasValue && EndTime.HasValue)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "End time must be after start time.",
                    [nameof(EndTime)]);
            }

            if (StartTime < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Start time cannot be in the past.",
                    [nameof(StartTime)]);
            }

            var duration = EndTime.Value - StartTime.Value;
            if (duration > TimeSpan.FromHours(6))
            {
                yield return new ValidationResult(
                    "A session cannot be longer than 6 hours.",
                    [nameof(EndTime)]);
            }
        }
    }
}
