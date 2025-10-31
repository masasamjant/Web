namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents middleware that reads session identifier from HTTP header and stores it using specified <see cref="IHttpContextValueSetter"/>.
    /// </summary>
    public sealed class ReadSessionIdentifierHeaderMiddleware : Middleware
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ReadSessionIdentifierHeaderMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> of the next action.</param>
        /// <param name="sessionIdentifierHeaderName">The name of HTTP header to read session identifier.</param>
        /// <param name="sessionIdentifierKey">The key to store session identifier.</param>
        public ReadSessionIdentifierHeaderMiddleware(RequestDelegate next, string? sessionIdentifierHeaderName, string? sessionIdentifierKey)
            : base(next)
        {
            SessionIdentifierHeaderName = sessionIdentifierHeaderName;
            SessionIdentifierKey = sessionIdentifierKey;
        }

        /// <summary>
        /// Gets the name of HTTP header to read session identifier.
        /// </summary>
        /// <remarks>If <c>null</c>, empty or only whitespace, then header is not read.</remarks>
        public string? SessionIdentifierHeaderName { get; }

        /// <summary>
        /// Gets the key to store session identifier using <see cref="IHttpContextValueSetter"/>.
        /// </summary>
        /// <remarks>If <c>null</c>, empty or only whitespace, then value not stored.</remarks>
        public string? SessionIdentifierKey { get; }

        /// <summary>
        /// Invoked when middleware is executed. Read HTTP header for session identifier and then stores it using <paramref name="setter"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="setter">The <see cref="IHttpContextValueSetter"/>.</param>
        public async Task InvokeAsync(HttpContext context, IHttpContextValueSetter setter)
        {
            var sessionIdentifierHeaderName = SessionIdentifierHeaderName;
            var sessionIdentifierKey = SessionIdentifierKey;

            if (!string.IsNullOrWhiteSpace(sessionIdentifierHeaderName) && !string.IsNullOrWhiteSpace(sessionIdentifierKey))
            {
                if (context.TryGetRequestHeaderValue(sessionIdentifierHeaderName, out var values) && values.Any())
                {
                    var sessionIdentifier = values.First();

                    if (!string.IsNullOrWhiteSpace(sessionIdentifier))
                        setter.SetHttpValue(context, sessionIdentifierKey, sessionIdentifier);
                }
            }

            await Next(context);
        }
    }
}
