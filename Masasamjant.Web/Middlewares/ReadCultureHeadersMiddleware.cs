using System.Globalization;

namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents middleware that read names of <see cref="CultureInfo.CurrentCulture"/> and <see cref="CultureInfo.CurrentUICulture"/> 
    /// from HTTP request headers and if valid names then sets cultures to those.
    /// </summary>
    public sealed class ReadCultureHeadersMiddleware : Middleware
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ReadCultureHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> of the next action.</param>
        /// <param name="currentCultureHttpHeader">The name of HTTP header to read name of <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="currentUICultureHttpHeader">The name of HTTP header to read name of <see cref="CultureInfo.CurrentUICulture"/>.</param>
        public ReadCultureHeadersMiddleware(RequestDelegate next, string? currentCultureHttpHeader, string? currentUICultureHttpHeader) 
            : base(next)
        {
            CurrentCultureHttpHeader = currentCultureHttpHeader;
            CurrentUICultureHttpHeader = currentUICultureHttpHeader;
        }

        /// <summary>
        /// Gets the name of HTTP header to read name of <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        /// <remarks>If <c>null</c>, empty or only whitespace, then culture name header is not read.</remarks>
        public string? CurrentCultureHttpHeader { get; }

        /// <summary>
        /// Gets the name of HTTP header to read name of <see cref="CultureInfo.CurrentUICulture"/>.
        /// </summary>
        /// <remarks>If <c>null</c>, empty or only whitespace, then culture name header is not read.</remarks>
        public string? CurrentUICultureHttpHeader { get; }

        /// <summary>
        /// Invoked when middleware is executed. Read HTTP headers and if contains culture names, then attempts to 
        /// set <see cref="CultureInfo.CurrentCulture"/> and <see cref="CultureInfo.CurrentUICulture"/> to cultures of those names.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <returns></returns>
        /// <exception cref="CultureNotFoundException">If culture specified in HTTP headers is not found.</exception>
        public async Task InvokeAsync(HttpContext context)
        {
            var currentCultureHttpHeader = CurrentCultureHttpHeader;
            var currentUICultureHttpHeader = CurrentUICultureHttpHeader;

            if (!string.IsNullOrWhiteSpace(currentCultureHttpHeader) &&
                context.TryGetRequestHeaderValue(currentCultureHttpHeader, out IEnumerable<string> values) && values.Any())
            {
                var cultureName = values.First();

                if (!string.IsNullOrWhiteSpace(cultureName))
                {
                    if (CultureHelper.IsAvailableCulture(cultureName) && CultureInfo.CurrentCulture.Name != cultureName)
                        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
                }
            }

            if (!string.IsNullOrWhiteSpace(currentUICultureHttpHeader) &&
                context.TryGetRequestHeaderValue(currentUICultureHttpHeader, out values) && values.Any())
            {
                var cultureName = values.First();

                if (!string.IsNullOrWhiteSpace(cultureName))
                {
                    if (CultureHelper.IsAvailableCulture(cultureName) && CultureInfo.CurrentUICulture.Name != cultureName)
                        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
                }
            }

            await Next(context);
        }
    }
}
