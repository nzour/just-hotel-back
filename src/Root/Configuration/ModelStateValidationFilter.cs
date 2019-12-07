using System.Collections.Generic;
using System.Linq;
using Application.Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Root.Configuration
{
    public class ModelStateValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var messages = new List<string>();

            foreach (var invalidValue in context.ModelState.Values)
            {
                messages.AddRange(invalidValue.Errors.Select(e => e.ErrorMessage));
            }

            throw new ValidationException(string.Join(string.Empty, messages));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // noop
        }
    }
}