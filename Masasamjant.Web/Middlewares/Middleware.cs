namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents abstract middleware.
    /// </summary>
    public abstract class Middleware
    {
        /// <summary>
        /// Intializes new instance of the <see cref="Middleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> of the next action.</param>
        protected Middleware(RequestDelegate next)
        {
            Next = next;
        }

        /// <summary>
        /// Gets the <see cref="RequestDelegate"/> of the next action.
        /// </summary>
        protected RequestDelegate Next { get; }
    }
}
