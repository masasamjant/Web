using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="IHttpContextAccessor"/> that provides HTTP context of <see cref="ControllerBase"/>.
    /// </summary>
    public sealed class ControllerHttpContextAccessor : IHttpContextAccessor
    {
        private readonly ControllerBase controller;

        /// <summary>
        /// Initializes new instance of the <see cref="ControllerHttpContextAccessor"/> class.
        /// </summary>
        /// <param name="controller">The <see cref="ControllerBase"/> to get HTTP context.</param>
        public ControllerHttpContextAccessor(ControllerBase controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Gets the <see cref="HttpContext"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">If attempt to set HTTP context.</exception>
        public HttpContext? HttpContext 
        { 
            get => controller.HttpContext;
            set => throw new NotSupportedException("Setting HTTP context is not supported."); 
        }
    }
}
