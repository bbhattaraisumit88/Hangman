using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hangman.Web.Helpers
{
    public static class Utility
    {
        public static ModelStateDictionary AddErrorToModelState(IdentityResult identityResult, ModelStateDictionary modelState)
        {
            foreach (var e in identityResult.Errors)
            {
                modelState.TryAddModelError("ModelState", e.Code + ", " + e.Description);
            }
            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(string description, ModelStateDictionary modelState)
        {
            modelState.TryAddModelError("ModelState", description);
            return modelState;
        }
    }
}