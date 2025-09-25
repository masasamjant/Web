using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Filters
{
    /// <summary>
    /// Represents <see cref="IActionFilter"/> that will work with <see cref="IApiController"/> interface 
    /// by notifying controller about action execution.
    /// </summary>
    public sealed class ApiControllerActionFilter : IActionFilter
    {
        /// <summary>
        /// Invoked after action is executed. If <see cref="ActionExecutedContext.Controller"/> is <see cref="IApiController"/>, 
        /// then invokes <see cref="IApiController.OnActionExecuted(ActionExecutedContext)"/>.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutedContext"/>.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnActionExecuted(context);
        }

        /// <summary>
        /// Invoked before action is executed. If <see cref="ActionExecutingContext.Controller"/> is <see cref="IApiController"/>, 
        /// then invokes <see cref="IApiController.OnActionExecuting(ActionExecutingContext)"/>.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutingContext"/>.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnActionExecuting(context);
        }
    }
}
