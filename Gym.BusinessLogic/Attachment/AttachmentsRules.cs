using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.AttachmentRules;

public class AttachmentsRules
{
    public const long MaxBytes=5 * 1024 * 1024; 
    public static readonly HashSet<string> AllowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png"
    };
}
