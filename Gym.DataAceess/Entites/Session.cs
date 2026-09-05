using System.Diagnostics.Contracts;

namespace Gym.DataAccess.Models;

public class Session : BaseEntity
{
    
    public string Description { get; set; } = default!;
    public int Capacity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = default!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
