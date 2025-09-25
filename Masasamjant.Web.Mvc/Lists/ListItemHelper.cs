namespace Masasamjant.Web.Lists
{
    /// <summary>
    /// Provides helper methods to generate <see cref="ListItem{T}"/> instances.
    /// </summary>
    public static class ListItemHelper
    {
        /// <summary>
        /// Create <see cref="ListItem{T}"/> instance from specified source items of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of data item.</typeparam>
        /// <param name="items">The data items.</param>
        /// <returns>A <see cref="ListItem{T}"/>s.</returns>
        public static IEnumerable<ListItem<T>> ToListItems<T>(IEnumerable<T> items)
        {
            var array = items.ToArray();

            for (int index = 0; index < array.Length; index++)
            {
                var item = array[index];
                bool first = index == 0;
                bool last = index == array.Length - 1;
                bool alternate = index % 2 == 0;
                yield return new ListItem<T>(item, alternate, index, first, last);
            }
        }
    }
}
