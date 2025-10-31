namespace Masasamjant.Web
{
    /// <summary>
    /// Represents component that provides <see cref="HttpSessionStorage"/>.
    /// </summary>
    public sealed class HttpSessionStorageProvider : ISessionStorageProvider
    {
        private readonly IHttpContextAccessor? httpContextAccessor;
        private readonly HttpContext? httpContext;

        /// <summary>
        /// Initializes new instance of the <see cref="HttpSessionStorageProvider"/> class.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        public HttpSessionStorageProvider(HttpContext context)
        {
            httpContext = context;
            httpContextAccessor = null;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="HttpSessionStorageProvider"/> class.
        /// </summary>
        /// <param name="accessor">The <see cref="IHttpContextAccessor"/>.</param>
        public HttpSessionStorageProvider(IHttpContextAccessor accessor)
        {
            httpContext = null;
            httpContextAccessor = accessor;
        }

        /// <summary>
        /// Gets the <see cref="ISessionStorage"/> implementation.
        /// </summary>
        /// <returns>A <see cref="ISessionStorage"/>.</returns>
        /// <exception cref="InvalidOperationException">If session storage cannot be provided.</exception>
        public HttpSessionStorage GetSessionStorage()
        {
            var context = GetHttpContext();

            if (context == null)
                throw new InvalidOperationException("HTTP context is not available.");

            return new HttpSessionStorage(context);
        }

        private HttpContext? GetHttpContext()
        {
            if (httpContext != null)
                return httpContext;

            if (httpContextAccessor != null)
                return httpContextAccessor.HttpContext;

            return null;
        }

        ISessionStorage ISessionStorageProvider.GetSessionStorage()
        {
            return this.GetSessionStorage();
        }
    }
}
