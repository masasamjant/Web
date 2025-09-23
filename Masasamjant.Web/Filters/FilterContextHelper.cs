using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Filters
{
    /// <summary>
    /// Provides helper methods to <see cref="FilterContext"/> class.
    /// </summary>
    public static class FilterContextHelper
    {
        /// <summary>
        /// Check if <see cref="FilterContext.Filters"/> contains <typeparamref name="TFilter"/> filter.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="context">The <see cref="FilterContext"/>.</param>
        /// <returns><c>true</c> if contains filter of <typeparamref name="TFilter"/>; <c>false</c> otherwise.</returns>
        public static bool ContainsFilter<TFilter>(this FilterContext context) where TFilter : IFilterMetadata
            => context.Filters.OfType<TFilter>().Any();

        /// <summary>
        /// Gets filter of <typeparamref name="TFilter"/> from <see cref="FilterContext.Filters"/>.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="context">The <see cref="FilterContext"/>.</param>
        /// <returns>A filters of <typeparamref name="TFilter"/>.</returns>
        public static IEnumerable<TFilter> GetFilters<TFilter>(this FilterContext context) where TFilter : IFilterMetadata
            => context.Filters.OfType<TFilter>();

        /// <summary>
        /// Gets the first filter of <typeparamref name="TFilter"/> from <see cref="FilterContext.Filters"/>.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="context">The <see cref="FilterContext"/>.</param>
        /// <returns>A first <typeparamref name="TFilter"/> or <c>null</c>.</returns>
        public static TFilter? GetFilter<TFilter>(this FilterContext context) where TFilter : IFilterMetadata
            => context.GetFilters<TFilter>().FirstOrDefault();
    }
}
