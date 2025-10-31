using Masasamjant.Http;
using Masasamjant.Http.Abstractions;

namespace Masasamjant.Web.Http.Interceptors
{
    /// <summary>
    /// Represents <see cref="HttpRequestInterceptor"/> that adds session identifier into HTTP headers.
    /// </summary>
    public sealed class SessionIdentifierHeaderInterceptor : HttpRequestInterceptor
    {
        private readonly ISessionStorageProvider sessionStorageProvider;

        /// <summary>
        /// Initializes new instance of the <see cref="SessionIdentifierHeaderInterceptor"/> class.
        /// </summary>
        /// <param name="sessionStorageProvider">The <see cref="ISessionStorageProvider"/>.</param>
        /// <param name="sessionIdentifierHeaderName">The name of session identifier HTTP header.</param>
        /// <remarks>If <paramref name="sessionIdentifierHeaderName"/> is <c>null</c>, empty or only whitespace, then session identifier header is not added.</remarks>
        public SessionIdentifierHeaderInterceptor(ISessionStorageProvider sessionStorageProvider, string? sessionIdentifierHeaderName)
        {
            this.sessionStorageProvider = sessionStorageProvider;
            SessionIdentifierHeaderName = sessionIdentifierHeaderName;
        }

        /// <summary>
        /// Gets the name of session identifier HTTP header.
        /// </summary>
        /// <remarks>If <c>null</c>, empty or only whitespace, then session identifier header is not added.</remarks>
        public string? SessionIdentifierHeaderName { get; }

        /// <summary>
        /// Intercepts specified <see cref="HttpGetRequest"/> before it it send and appends session identifier header.
        /// </summary>
        /// <param name="request">The <see cref="HttpGetRequest"/> to intercept.</param>
        /// <returns>A <see cref="HttpRequestInterception"/> after this interceptor.</returns>
        public override Task<HttpRequestInterception> InterceptAsync(HttpGetRequest request)
        {
            return Task.FromResult(AddSessionIdentifierHeader(request));
        }

        /// <summary>
        /// Intercepts specified <see cref="HttpPostRequest"/> before it it send and appends session identifier header.
        /// </summary>
        /// <param name="request">The <see cref="HttpPostRequest"/> to intercept.</param>
        /// <returns>A <see cref="HttpRequestInterception"/> after this interceptor.</returns>
        public override Task<HttpRequestInterception> InterceptAsync(HttpPostRequest request)
        {
            return Task.FromResult(AddSessionIdentifierHeader(request));
        }

        private HttpRequestInterception AddSessionIdentifierHeader(HttpRequest request)
        {
            if (!string.IsNullOrWhiteSpace(SessionIdentifierHeaderName) && 
                !request.Headers.Contains(SessionIdentifierHeaderName))
            {
                var sessionIdentifier = GetSessionIdentifier();
                request.Headers.Add(SessionIdentifierHeaderName, sessionIdentifier);
            }

            return HttpRequestInterception.Continue;
        }

        private string GetSessionIdentifier()
        {
            var storage = sessionStorageProvider.GetSessionStorage();
            return storage.GetSessionIdentifier();
        }
    }
}
