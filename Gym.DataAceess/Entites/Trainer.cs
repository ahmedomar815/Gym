using Gym.DataAccess.Models.Enums;

namespace Gym.DataAccess.Models;

public class Trainer : User
{
    public Speciality Speciality { get; set; }
    

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
