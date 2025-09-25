using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="ControllerBase"/> that implements <see cref="IApiController"/> interface.
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase, IApiController
    {
        /// <summary>
        /// Invoked after action is executed.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutedContext"/>.</param>
        protected virtual void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        /// <summary>
        /// Invoked before action is executed.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutingContext"/>.</param>
        protected virtual void OnActionExecuting(ActionExecutingContext context)
        {
            return;
        }

        /// <summary>
        /// Invoked after result is executed.
        /// </summary>
        /// <param name="context">The <see cref="ResultExecutedContext"/>.</param>
        protected virtual void OnResultExecuted(ResultExecutedContext context)
        {
            return;
        }

        /// <summary>
        /// Invoked before result is executed.
        /// </summary>
        /// <param name="context">The <see cref="ResultExecutingContext"/>.</param>
        protected virtual void OnResultExecuting(ResultExecutingContext context)
        {
            return;
        }

        void IApiController.OnActionExecuted(ActionExecutedContext context)
        {
            OnActionExecuted(context);
        }

        void IApiController.OnActionExecuting(ActionExecutingContext context)
        {
            OnActionExecuting(context);
        }

        void IApiController.OnResultExecuted(ResultExecutedContext context)
        {
            OnResultExecuted(context);
        }

        void IApiController.OnResultExecuting(ResultExecutingContext context)
        {
            OnResultExecuting(context);
        }
    }
}
