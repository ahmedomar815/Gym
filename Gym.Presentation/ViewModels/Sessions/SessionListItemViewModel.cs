using System.ComponentModel.DataAnnotations;

namespace Gym.Presentation.ViewModels.Sessions;

public sealed class SessionListItemViewModel 
{
    public int Id { get; init; }

    [Required]
    [StringLength(100)]
    public string CategoryName { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Description { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string TrainerName { get; init; } = string.Empty;

    [Required]
    public DateTime StartTime { get; init; }

    [Required]
    public DateTime EndTime { get; init; }

    [Range(1, 25)]
    public int Capacity { get; init; }

    [Range(0, 25)]
    public int AvailableSlots { get; init; }

    public string Status => DateTime.Now switch
    {
        var now when now < StartTime => "Upcoming",
        var now when now >= EndTime => "Completed",
        _ => "Ongoing"
    };

    public string DateDisplay => StartTime.ToString("dd MMM yyyy");

    public string TimeRangeDisplay => $"{StartTime:hh:mm tt} - {EndTime:hh:mm tt}";

    public TimeSpan Duration => EndTime - StartTime;

    
}
