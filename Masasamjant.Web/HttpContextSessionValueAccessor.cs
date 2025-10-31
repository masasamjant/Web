namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="HttpContextValueAccessor"/> that gets or sets value to <see cref="HttpContext.Session"/>.
    /// </summary>
    public sealed class HttpContextSessionValueAccessor : HttpContextValueAccessor
    {
        /// <summary>
        /// Gets the value stored in <see cref="HttpContext.Session"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <returns>A stored value or <c>null</c>.</returns>
        public override string? GetHttpValue(HttpContext context, string key)
        {
            return GetSession(context).GetString(key);
        }

        /// <summary>
        /// Sets the value to <see cref="HttpContext.Session"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value to set.</param>
        public override void SetHttpValue(HttpContext context, string key, string value)
        {
            GetSession(context).SetString(key, value);
        }

        private static HttpSessionStorage GetSession(HttpContext context)
            => new HttpSessionStorage(context);
    }
}
