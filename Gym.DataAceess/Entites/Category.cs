using Gym.DataAccess.Models.Enums;

namespace Gym.DataAccess.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Speciality Speciality { get; set; } = default!;
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<Trainer> Trainers { get; set; } = [];
}
