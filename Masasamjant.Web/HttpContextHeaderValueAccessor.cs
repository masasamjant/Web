namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="HttpContextValueAccessor"/> that gets value from HTTP context request headers 
    /// and sets value to HTTP context response headers.
    /// </summary>
    public sealed class HttpContextHeaderValueAccessor : HttpContextValueAccessor
    {
        /// <summary>
        /// Gets the value stored in request header.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <returns>A stored value or <c>null</c>.</returns>
        public override string? GetHttpValue(HttpContext context, string key)
        {
            return context.TryGetRequestHeaderValue(key, out var values) ? values.FirstOrDefault() : null;
        }

        /// <summary>
        /// Sets the value to response header.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value to set.</param>
        public override void SetHttpValue(HttpContext context, string key, string value)
        {
            context.Response.Headers.Append(key, value);
        }
    }
}
