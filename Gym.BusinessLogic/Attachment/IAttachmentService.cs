using Gym.BusinessLogic.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.AttachmentRules;

public interface IAttachmentService
{
    Task<Result<string>> SaveAsync(IFormFile file, string category, CancellationToken cancellationToken);
     Task<Result> DeleteAsync(string storageKey, CancellationToken cancellationToken);
    Task<Result<Stream>> GetAsync(string storageKey, CancellationToken cancellationToken);
}
