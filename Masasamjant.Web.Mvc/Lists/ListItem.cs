namespace Masasamjant.Web.Lists
{
    /// <summary>
    /// Represents list item in view like table row.
    /// </summary>
    /// <typeparam name="T">The type of the data item.</typeparam>
    public sealed class ListItem<T>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ListItem{T}"/> class.
        /// </summary>
        /// <param name="item">The data time.</param>
        /// <param name="alternate"><c>true</c> if alternate; <c>false</c> otherwise.</param>
        /// <param name="order">The order.</param>
        /// <param name="first"><c>true</c> if first item; <c>false</c> otherwise.</param>
        /// <param name="last"><c>true</c> if last item; <c>false</c> otherwise.</param>
        public ListItem(T item, bool alternate, int order, bool first, bool last)
        {
            Item = item;
            IsAlternate = alternate;
            Order = order;
            IsFirst = first;
            IsLast = last;
        }

        /// <summary>
        /// Gets whether or not item is alternate item. 
        /// </summary>
        public bool IsAlternate { get; }

        /// <summary>
        /// Gets the data item.
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// Gets display order. Smaller value means higher order.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Gets whether or not represents first item.
        /// </summary>
        public bool IsFirst { get; }

        /// <summary>
        /// Gets whether or not represents last item.
        /// </summary>
        public bool IsLast { get; }
    }
}
