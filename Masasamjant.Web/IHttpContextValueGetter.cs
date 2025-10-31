namespace Masasamjant.Web
{
    /// <summary>
    /// Represents component to get value associated with <see cref="HttpContext"/>.
    /// </summary>
    public interface IHttpContextValueGetter
    {
        /// <summary>
        /// Gets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <returns>A stored value or <c>null</c>.</returns>
        string? GetHttpValue(HttpContext context, string key);
    }
}
