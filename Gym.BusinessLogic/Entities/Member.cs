using Gym.DataAccess.Models.Enums;

namespace Gym.DataAccess.Models;

public class Member : User
{
   
    
    public DateTime JoinDate {  get; set; }
    public HealthyRecord HealthyRecord { get; set; } = default!;
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
