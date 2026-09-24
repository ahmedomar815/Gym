using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAceess.Data.Identity;

public class ApplicationRole:IdentityRole<Guid>
{
    public string DisplayName { get; set; } = default!;
}
