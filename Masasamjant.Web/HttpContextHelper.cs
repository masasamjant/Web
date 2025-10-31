namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods related to <see cref="HttpContext"/>.
    /// </summary>
    public static class HttpContextHelper
    {
        /// <summary>
        /// Get values of request HTTP header specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="name">The HTTP header name.</param>
        /// <returns>A HTTP header values.</returns>
        public static IEnumerable<string> GetRequestHeaderValues(this HttpContext context, string name)
            => GetHeaderValues(context.Request.Headers, name);

        /// <summary>
        /// Get values of response HTTP header specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="name">The HTTP header name.</param>
        /// <returns>A HTTP header values.</returns>
        public static IEnumerable<string> GetResponseHeaderValues(this HttpContext context, string name)
            => GetHeaderValues(context.Response.Headers, name);

        /// <summary>
        /// Get values of HTTP header specified by name.
        /// </summary>
        /// <param name="headers">The <see cref="IHeaderDictionary"/>.</param>
        /// <param name="name">The HTTP header name.</param>
        /// <returns>A HTTP header values.</returns>
        public static IEnumerable<string> GetHeaderValues(this IHeaderDictionary headers, string name)
        {
            if (headers.TryGetValue(name, out var values) && values.Count > 0)
            {
                foreach (var value in values)
                    yield return value ?? string.Empty;
            }
        }

        /// <summary>
        /// Try get value of request HTTP header specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="name">The HTTP header name.</param>
        /// <param name="values">The HTTP header values, if returns <c>true</c>.</param>
        /// <returns><c>true</c> if headers has specified header; <c>false</c> otherwise.</returns>
        public static bool TryGetRequestHeaderValue(this HttpContext context, string name, out IEnumerable<string> values)
            => TryGetHeaderValue(context.Request.Headers, name, out values);

        /// <summary>
        /// Try get value of response HTTP header specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="name">The HTTP header name.</param>
        /// <param name="values">The HTTP header values, if returns <c>true</c>.</param>
        /// <returns><c>true</c> if headers has specified header; <c>false</c> otherwise.</returns>
        public static bool TryGetResponseHeaderValue(this HttpContext context, string name, out IEnumerable<string> values)
            => TryGetHeaderValue(context.Response.Headers, name, out values);

        /// <summary>
        /// Try get value of HTTP header specified by name.
        /// </summary>
        /// <param name="headers">The <see cref="IHeaderDictionary"/>.</param>
        /// <param name="name">The HTTP header name.</param>
        /// <param name="values">The HTTP header values, if returns <c>true</c>.</param>
        /// <returns><c>true</c> if headers has specified header; <c>false</c> otherwise.</returns>
        public static bool TryGetHeaderValue(this IHeaderDictionary headers, string name, out IEnumerable<string> values)
        {
            if (headers.TryGetValue(name, out var headerValue))
            {
                if (headerValue.Count > 0)
                    values = headerValue.ToArray<string>();
                else
                    values = [];

                return true;
            }

            values = [];
            return false;
        }

        /// <summary>
        /// Gets request cookie value or <c>null</c>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <returns>A cookie value or <c>null</c>.</returns>
        public static string? GetCookieValue(this HttpContext context, string cookieName)
            => GetCookieValue(context.Request.Cookies, cookieName);

        /// <summary>
        /// Gets request cookie value or <c>null</c>.
        /// </summary>
        /// <param name="cookies">The <see cref="IRequestCookieCollection"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <returns>A cookie value or <c>null</c>.</returns>
        public static string? GetCookieValue(this IRequestCookieCollection cookies, string cookieName)
            => cookies.TryGetCookieValue(cookieName, out var cookieValue) ? cookieValue : null;

        /// <summary>
        /// Try get request cookie value.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value, if returns <c>true</c>.</param>
        /// <returns><c>true</c> if cookies contains specified cookie; <c>false</c> otherwise.</returns>
        public static bool TryGetCookieValue(this HttpContext context, string cookieName, out string? cookieValue)
            => TryGetCookieValue(context.Request.Cookies, cookieName, out cookieValue);

        /// <summary>
        /// Try get request cookie value.
        /// </summary>
        /// <param name="cookies">The <see cref="IRequestCookieCollection"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value, if returns <c>true</c>.</param>
        /// <returns><c>true</c> if cookies contains specified cookie; <c>false</c> otherwise.</returns>
        public static bool TryGetCookieValue(this IRequestCookieCollection cookies, string cookieName, out string? cookieValue)
            => cookies.TryGetValue(cookieName, out cookieValue);

        /// <summary>
        /// Set response cookie with specified name and value using provided settings.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <param name="path">The cookie path or <c>null</c> to use default.</param>
        /// <param name="expires">The expiration date and time.</param>
        /// <param name="maxAge">The max age.</param>
        /// <param name="httpOnly"><c>true</c>, default, if only for HTTP; <c>false</c> otherwise.</param>
        /// <param name="secure"><c>true</c> to transmit only in HTTPS; <c>false</c>, default, otherwise.</param>
        /// <param name="essential"><c>true</c> if cookie is essential; <c>false</c>, default, otherwise.</param>
        /// <param name="domain">The domain associated with cookie.</param>
        /// <param name="sameSiteMode">The <see cref="SameSiteMode"/>. <see cref="SameSiteMode.Unspecified"/> is default.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="sameSiteMode"/> is not defined.</exception>
        public static void SetCookieValue(this HttpContext context, string cookieName, string cookieValue,
            string? path = null, DateTimeOffset? expires = null, TimeSpan? maxAge = null, bool httpOnly = true, bool secure = false, bool essential = false,
            string? domain = null, SameSiteMode sameSiteMode = SameSiteMode.Unspecified)
            => SetCookieValue(context.Response.Cookies, cookieName, cookieValue, path, expires, maxAge, httpOnly, secure, essential, domain, sameSiteMode);

        /// <summary>
        /// Set response cookie with specified name and value using provided settings.
        /// </summary>
        /// <param name="cookies">The <see cref="IResponseCookies"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <param name="path">The cookie path or <c>null</c> to use default.</param>
        /// <param name="expires">The expiration date and time.</param>
        /// <param name="maxAge">The max age.</param>
        /// <param name="httpOnly"><c>true</c>, default, if only for HTTP; <c>false</c> otherwise.</param>
        /// <param name="secure"><c>true</c> to transmit only in HTTPS; <c>false</c>, default, otherwise.</param>
        /// <param name="essential"><c>true</c> if cookie is essential; <c>false</c>, default, otherwise.</param>
        /// <param name="domain">The domain associated with cookie.</param>
        /// <param name="sameSiteMode">The <see cref="SameSiteMode"/>. <see cref="SameSiteMode.Unspecified"/> is default.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="sameSiteMode"/> is not defined.</exception>
        public static void SetCookieValue(this IResponseCookies cookies, string cookieName, string cookieValue, 
            string? path = null, DateTimeOffset? expires = null, TimeSpan? maxAge = null, bool httpOnly = true, bool secure = false, bool essential = false, 
            string? domain = null, SameSiteMode sameSiteMode = SameSiteMode.Unspecified)
        {
            if (!Enum.IsDefined(sameSiteMode))
                throw new ArgumentException("The value is not defined.", nameof(sameSiteMode));

            var options = new CookieOptions()
            {
                Domain = domain,
                Expires = expires,
                MaxAge = maxAge,
                HttpOnly = httpOnly,
                Secure = secure,
                IsEssential = essential,
                SameSite = sameSiteMode
            };

            if (!string.IsNullOrWhiteSpace(path))
                options.Path = path;

            cookies.Append(cookieName, cookieValue, options);
        }
    }
}
