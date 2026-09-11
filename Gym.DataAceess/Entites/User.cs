using Gym.DataAccess.Models.Enums;

namespace Gym.DataAccess.Models;

public class User : BaseEntity
{
    public string? PhotoUrl { get; set; }
    public string Name { get; set; } = default!;

    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Gender Gender { get; set; }
    public Address Address { get; set; } = new();
    public bool IsActive { get; set; }
}
