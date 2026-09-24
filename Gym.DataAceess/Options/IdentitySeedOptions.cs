using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAceess.Options;

public  class IdentitySeedOptions
{
    public AdminSeedOptions Admin { get; set; } = new();
    public AdminSeedOptions SuperAdmin { get; set; } = new();
}
