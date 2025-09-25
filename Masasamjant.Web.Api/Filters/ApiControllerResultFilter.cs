using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Filters
{
    /// <summary>
    /// Represents <see cref="IResultFilter"/> that will work with <see cref="IApiController"/> interface 
    /// by notifying controller about result execution.
    /// </summary>
    public sealed class ApiControllerResultFilter : IResultFilter
    {
        /// <summary>
        /// Invoked after result is executed. If <see cref="ResultExecutedContext.Controller"/> is <see cref="IApiController"/>, 
        /// then invokes <see cref="IApiController.OnResultExecuted(ResultExecutedContext)"/>.
        /// </summary>
        /// <param name="context">The <see cref="ResultExecutedContext"/>.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnResultExecuted(context);
        }

        /// <summary>
        /// Invoked after result is executed. If <see cref="ResultExecutingContext.Controller"/> is <see cref="IApiController"/>, 
        /// then invokes <see cref="IApiController.OnResultExecuting(ResultExecutingContext)"/>.
        /// </summary>
        /// <param name="context">The <see cref="ResultExecutingContext"/>.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is IApiController controller)
                controller.OnResultExecuting(context);
        }
    }
}
