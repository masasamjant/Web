namespace Masasamjant.Web
{
    /// <summary>
    /// Represents component that store value associated with <see cref="HttpContent"/>.
    /// </summary>
    public interface IHttpContextValueSetter
    {
        /// <summary>
        /// Sets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value to set.</param>
        void SetHttpValue(HttpContext context, string key, string value);
    }
}
