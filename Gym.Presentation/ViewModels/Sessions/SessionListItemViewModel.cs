using Gym.DataAccess.Models;
using Gym.Presentation.Enums;
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

    [Range(1, 25)]
    public int CountBooking { get; init; }

    public string TimeRangeDisplay =>
    $"{StartTime:HH:mm} - {EndTime:HH:mm}";

    public SessionStatus Status => this switch
    {
        var s when s.StartTime > DateTime.Now => SessionStatus.Upcoming,
        var s when s.EndTime < DateTime.Now => SessionStatus.Completed,
        _ => SessionStatus.Ongoing
    };

    public DateOnly Date => DateOnly.FromDateTime(StartTime);

    public TimeSpan Duration => EndTime - StartTime;

    
}
