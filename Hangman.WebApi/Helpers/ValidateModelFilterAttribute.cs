using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hangman.Web.Helpers
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionExecutedContext)
        {
            if (!actionExecutedContext.ModelState.IsValid)
            {
                actionExecutedContext.Result = new BadRequestObjectResult(actionExecutedContext.ModelState);
            }
        }
    }
}