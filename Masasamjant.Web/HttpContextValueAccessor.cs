
namespace Masasamjant.Web
{
    /// <summary>
    /// Represents component that gets or sets value associated with <see cref="HttpContext"/>.
    /// </summary>
    public abstract class HttpContextValueAccessor : IHttpContextValueGetter, IHttpContextValueSetter
    {
        /// <summary>
        /// Gets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <returns>A stored value or <c>null</c>.</returns>
        public abstract string? GetHttpValue(HttpContext context, string key);

        /// <summary>
        /// Sets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value to set.</param>
        public abstract void SetHttpValue(HttpContext context, string key, string value);
    }
}
