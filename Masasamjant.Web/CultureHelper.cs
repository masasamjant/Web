using System.Globalization;

namespace Masasamjant.Web
{
    /// <summary>
    /// Represents helper to work with cultures.
    /// </summary>
    public sealed class CultureHelper
    {
        private static readonly CultureHelper instance = new();

        /// <summary>
        /// Check if culture specified by name exist.
        /// </summary>
        /// <param name="cultureName">The culture name.</param>
        /// <returns><c>true</c> if culture with specified name exist; <c>false</c> otherwise.</returns>
        public static bool IsAvailableCulture(string cultureName)
        {
            return instance.GetAvailableCultures().Any(x => x.Name == cultureName);
        }

        /// <summary>
        /// Gets the culture specified by name if exist or <c>null</c> otherwise.
        /// </summary>
        /// <param name="cultureName">The culture name.</param>
        /// <returns>A <see cref="CultureInfo"/> or <c>null</c>, if culture not exist.</returns>
        public static CultureInfo? GetCulture(string cultureName)
        {
            return instance.GetAvailableCultures().FirstOrDefault(x => x.Name == cultureName);
        }

        private readonly Lazy<CultureInfo[]> lazyCultures;

        private CultureHelper()
        {
            lazyCultures = new Lazy<CultureInfo[]>(() => CultureInfo.GetCultures(CultureTypes.AllCultures));
        }

        private CultureInfo[] GetAvailableCultures()
        {
            return lazyCultures.Value;
        }
    }
}
