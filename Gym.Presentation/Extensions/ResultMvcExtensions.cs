using Gym.BusinessLogic.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gym.Presentation.Extensions;

public static class ResultMvcExtensions
{
    public static void AddToModelState(this Result result, ModelStateDictionary modelState)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccess)
        {
            return;
        }

        var key = result.Error.Type is ErrorType.Validation or ErrorType.Conflict
            ? result.Error.Code
            : string.Empty;

        modelState.AddModelError(key, result.Error.Description);
    }
}
