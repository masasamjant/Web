namespace Masasamjant.Web.Ajax
{
    /// <summary>
    /// Represents <see cref="ViewModel"/> that implements <see cref="IAjaxForm{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the form data object.</typeparam>
    public class AjaxFormViewModel<T> : ViewModel, IAjaxForm<T> where T : class
    {
        private AjaxUpdate ajaxUpdate = AjaxUpdate.Replace;
        private AjaxErrorDisplay errorDisplay = AjaxErrorDisplay.None;

        /// <summary>
        /// Initializes new default instance of the <see cref="AjaxFormViewModel{T}"/> class.
        /// </summary>
        public AjaxFormViewModel()
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="AjaxFormViewModel{T}"/> class.
        /// </summary>
        /// <param name="data">The form data.</param>
        /// <param name="ajaxUpdateElementId">The value of <c>id</c> attribute of update element.</param>
        /// <param name="ajaxErrorElementId">The value of <c>id</c> attribute of error element.</param>
        /// <param name="update">How target element is updated.</param>
        /// <param name="error">How error is displayed.</param>
        public AjaxFormViewModel(T? data, string ajaxUpdateElementId, string ajaxErrorElementId, AjaxUpdate update, AjaxErrorDisplay error)
        {
            if (!Enum.IsDefined(update))
                throw new ArgumentException("The value is not defined.", nameof(update));

            if (!Enum.IsDefined(error))
                throw new ArgumentException("The value is not defined.", nameof(error));

            AjaxUpdate = update;
            Data = data;
            ErrorDisplay = error;
            AjaxUpdateElementId = ajaxUpdateElementId;
            AjaxErrorElementId = ajaxErrorElementId;
        }

        /// <summary>
        /// Gets or sets form data.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Gets or sets how target element is updated.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        public AjaxUpdate AjaxUpdate 
        {
            get { return ajaxUpdate; }
            set 
            {
                if (ajaxUpdate != value)
                {
                    if (!Enum.IsDefined(value))
                        throw new ArgumentException("Value is not defined.", nameof(AjaxUpdate));

                    ajaxUpdate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets how ajax error is displayed.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        public AjaxErrorDisplay ErrorDisplay 
        {
            get { return errorDisplay; }
            set
            {
                if (errorDisplay != value)
                {
                    if (!Enum.IsDefined(value))
                        throw new ArgumentException("Value is not defined.", nameof(ErrorDisplay));

                    errorDisplay = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of update element.
        /// </summary>
        public string AjaxUpdateElementId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of error element.
        /// </summary>
        public string AjaxErrorElementId { get; set; } = string.Empty;
    }
}
