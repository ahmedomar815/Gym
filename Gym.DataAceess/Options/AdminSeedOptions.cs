using System.ComponentModel.DataAnnotations;

namespace Gym.DataAceess.Options;

public class AdminSeedOptions
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}