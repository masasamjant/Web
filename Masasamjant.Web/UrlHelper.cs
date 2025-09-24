namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods to work with URLs and URIs.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Check is specified URL string is absolute URI and optionally one of the specified schemes. If <paramref name="schemes"/> is <c>null</c> 
        /// or empty, then <paramref name="url"/> must be absolute URI. Otherwise <see cref="Uri.Scheme"/> must be one of in <paramref name="schemes"/>.
        /// </summary>
        /// <param name="url">The URL string.</param>
        /// <param name="schemes">The valid schemes.</param>
        /// <returns><c>true</c> if <paramref name="url"/> is valid absolute URI and, optionally, one of the schemes;<c>false</c> otherwise.</returns>
        public static bool IsValidUrl(string url, IEnumerable<string>? schemes = null)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                if (schemes != null && schemes.Any())
                    return schemes.Contains(uri.Scheme);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Check is specified URL string absolute URI with HTTP or HTTPS scheme.
        /// </summary>
        /// <param name="url">The URL string.</param>
        /// <returns><c>true</c> if <paramref name="url"/> is absolute URI with HTTP or HTTPS scheme; <c>false</c> otherwise.</returns>
        public static bool IsValidHttpUrl(string url) => IsValidUrl(url, [Uri.UriSchemeHttp, Uri.UriSchemeHttps]);
    }
}
