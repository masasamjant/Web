namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="HttpContextValueAccessor"/> that gets or sets value to <see cref="HttpContext.Items"/>.
    /// </summary>
    public sealed class HttpContextItemValueAccessor : HttpContextValueAccessor
    {
        /// <summary>
        /// Gets the value stored in <see cref="HttpContext.Items"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <returns>A stored value or <c>null</c>.</returns>
        public override string? GetHttpValue(HttpContext context, string key)
        {
            if (context.Items.TryGetValue(key, out var value) && value is string s)
                return s;

            return null;
        }

        /// <summary>
        /// Sets the value to <see cref="HttpContext.Items"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value to set.</param>
        public override void SetHttpValue(HttpContext context, string key, string value)
        {
            context.Items[key] = value;
        }
    }
}
