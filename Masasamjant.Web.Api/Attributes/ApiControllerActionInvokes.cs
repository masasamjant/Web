using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Attributes
{
    /// <summary>
    /// Defines what <see cref="IApiController"/> methods <see cref="ApiControllerActionAttribute"/> invokes.
    /// </summary>
    [Flags]
    public enum ApiControllerActionInvokes : int
    {
        /// <summary>
        /// Invoke none.
        /// </summary>
        None = 0,

        /// <summary>
        /// Invoke <see cref="IApiController.OnActionExecuting(ActionExecutingContext)"/>.
        /// </summary>
        ActionExecuting = 1,

        /// <summary>
        /// Invoke <see cref="IApiController.OnActionExecuted(ActionExecutedContext)"/>.
        /// </summary>
        ActionExecuted = 2,

        /// <summary>
        /// Invoke <see cref="IApiController.OnResultExecuting(ResultExecutingContext)"/>.
        /// </summary>
        ResultExecuting = 4,

        /// <summary>
        /// Invoke <see cref="IApiController.OnResultExecuted(ResultExecutedContext)"/>.
        /// </summary>
        ResultExecuted = 8
    }
}
