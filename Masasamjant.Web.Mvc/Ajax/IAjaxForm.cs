namespace Masasamjant.Web.Ajax
{
    /// <summary>
    /// Represents HTML form that use AJAX.
    /// </summary>
    /// <typeparam name="T">The type of the form data object.</typeparam>
    public interface IAjaxForm<T> where T : class
    {
        /// <summary>
        /// Gets or sets form data.
        /// </summary>
        T? Data { get; set; }

        /// <summary>
        /// Gets or sets how target element is updated.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        AjaxUpdate AjaxUpdate { get; set; }

        /// <summary>
        /// Gets or sets how ajax error is displayed.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        AjaxErrorDisplay ErrorDisplay { get; set; }

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of update element.
        /// </summary>
        string AjaxUpdateElementId { get; set; }

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of error element.
        /// </summary>
        string AjaxErrorElementId { get; set; }
    }
}
