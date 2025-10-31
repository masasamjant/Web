namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Provides helper methods to register middlewares.
    /// </summary>
    public static class MiddlewareHelper
    {
        /// <summary>
        /// Register <see cref="ReadCultureHeadersMiddleware"/> with specified <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="currentCultureHttpHeader">The name of HTTP header to read name of <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="currentUICultureHttpHeader">The name of HTTP header to read name of <see cref="CultureInfo.CurrentUICulture"/>.</param>
        /// <returns>A <paramref name="builder"/>.</returns>
        public static IApplicationBuilder UseReadCultureHeaders(this IApplicationBuilder builder, string? currentCultureHttpHeader, string? currentUICultureHttpHeader)
        {
            return builder.UseMiddleware<ReadCultureHeadersMiddleware>(currentCultureHttpHeader, currentUICultureHttpHeader);
        }

        /// <summary>
        /// Register <see cref="ReadSessionIdentifierHeaderMiddleware"/> with specified <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="sessionIdentifierHeaderName">The name of HTTP header to read session identifier.</param>
        /// <param name="sessionIdentifierKey">The key to store session identifier.</param>
        /// <returns>A <paramref name="builder"/>.</returns>
        public static IApplicationBuilder UseReadSessionIdentifierHeader(this IApplicationBuilder builder, string? sessionIdentifierHeaderName, string? sessionIdentifierKey) 
        {
            return builder.UseMiddleware<ReadSessionIdentifierHeaderMiddleware>(sessionIdentifierHeaderName, sessionIdentifierKey);
        }
    }
}
