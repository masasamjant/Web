using Masasamjant.Http.Abstractions;
using Masasamjant.Web.Http.Interceptors;

namespace Masasamjant.Web.Http
{
    /// <summary>
    /// Provides extension methods to <see cref="IHttpClient"/> interface.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Adds <see cref="SessionIdentifierHeaderInterceptor"/> to specified <see cref="IHttpClient"/>.
        /// </summary>
        /// <param name="httpClient">The <see cref="IHttpClient"/>.</param>
        /// <param name="sessionIdentifierHeaderName">The name of session identifier HTTP header.</param>
        /// <returns>A <paramref name="httpClient"/>.</returns>
        /// <remarks>If <paramref name="sessionIdentifierHeaderName"/> is <c>null</c>, empty or only whitespace, then session identifier header is not added.</remarks>
        public static IHttpClient AddSessionIdentifierHeaderInterceptor(this IHttpClient httpClient, ISessionStorageProvider sessionStorageProvider, string? sessionIdentifierHeaderName)
        {
            var interceptor = new SessionIdentifierHeaderInterceptor(sessionStorageProvider, sessionIdentifierHeaderName);
            httpClient.HttpGetRequestInterceptors.Add(interceptor);
            httpClient.HttpPostRequestInterceptors.Add(interceptor);
            return httpClient;
        }
    }
}
